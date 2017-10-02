using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {

	public const float RATE = 1.0f;

	[HideInInspector]
	public bool locationServicesIsRunning = false;

	public GameObject mainMap;
	public GameObject newMap;

	public GameObject player;
	public GeoPoint playerGeoPosition;
	public PlayerLocationService player_loc;
	public float lat;
	public float lon;

	public enum PlayerStatus { TiedToDevice, FreeFromDevice }

	private ObjectPosition playerPositionObject;
	private float timeAcum;
	private PlayerStatus _playerStatus;
	public PlayerStatus playerStatus
	{
		get { return _playerStatus; }
		set { _playerStatus = value; }
	}

	void Awake (){

		Time.timeScale = 1;
		playerStatus = PlayerStatus.TiedToDevice;

		player_loc = player.GetComponent<PlayerLocationService>();
		newMap.GetComponent<MeshRenderer>().enabled = false;
		newMap.SetActive (false);

		playerPositionObject = player.GetComponent<ObjectPosition> ();
	}

	public GoogleStaticMap getMainMapMap () {
		return mainMap.GetComponent<GoogleStaticMap> ();
	}

	public GoogleStaticMap getNewMapMap () {
		return newMap.GetComponent<GoogleStaticMap> ();
	}

	IEnumerator Start () {

		getMainMapMap ().initialize ();
		yield return StartCoroutine (player_loc._StartLocationService ());
		StartCoroutine (player_loc.RunLocationService ());

		locationServicesIsRunning = player_loc.locServiceIsRunning;
		Debug.Log ("Player loc from GameManager: " + player_loc.loc);
		getMainMapMap ().centerMercator = getMainMapMap ().tileCenterMercator (player_loc.loc);
		getMainMapMap ().DrawMap ();

		mainMap.transform.localScale = Vector3.Scale (
			new Vector3 (getMainMapMap ().mapRectangle.getWidthMeters (), getMainMapMap ().mapRectangle.getHeightMeters (), 1.0f),
			new Vector3 (getMainMapMap ().realWorldtoUnityWorldScale.x, getMainMapMap ().realWorldtoUnityWorldScale.y, 1.0f));

		player.GetComponent<ObjectPosition> ().setPositionOnMap (player_loc.loc);

		GameObject[] objectsOnMap = GameObject.FindGameObjectsWithTag ("ObjectOnMap");

		foreach (GameObject obj in objectsOnMap) {
			obj.GetComponent<ObjectPosition> ().setPositionOnMap ();
		}
    }

    void Update () {

		//Este es el codigo que termina posicionando al user de desktop cada RATE segundos en la posicion que indican
		//las variables lat y lon, las cuales son alteradas por InputController
		if (SystemInfo.deviceType == DeviceType.Desktop) {
			timeAcum += Time.deltaTime;
			if (timeAcum > RATE) {
				timeAcum -= RATE;
				playerPositionObject.setPositionOnMap (lat, lon);
			}
		}

		if(!locationServicesIsRunning){
			//TODO: Show location service is not enabled error.
			return;
		}

		// playerGeoPosition = getMainMapMap ().getPositionOnMap(new Vector2(player.transform.position.x, player.transform.position.z));
		playerGeoPosition = new GeoPoint();
		// GeoPoint playerGeoPosition = getMainMapMap ().getPositionOnMap(new Vector2(player.transform.position.x, player.transform.position.z));
		if (playerStatus == PlayerStatus.TiedToDevice) {
			playerGeoPosition = player_loc.loc;
			player.GetComponent<ObjectPosition> ().setPositionOnMap (playerGeoPosition);
		} else if (playerStatus == PlayerStatus.FreeFromDevice){
			playerGeoPosition = getMainMapMap ().getPositionOnMap(new Vector2(player.transform.position.x, player.transform.position.z));
		}


		GoogleStaticMap.MyPoint tileCenterMercator;
		if (SystemInfo.deviceType == DeviceType.Desktop) {
			tileCenterMercator = getMainMapMap ().tileCenterMercator (playerPositionObject.getPositionOnMap());
		} else {
			tileCenterMercator = getMainMapMap ().tileCenterMercator (playerGeoPosition);
		}

		if(!getMainMapMap ().centerMercator.isEqual(tileCenterMercator)) {
			Debug.Log (getMainMapMap().centerMercator.ToString() + ", " + tileCenterMercator.ToString());

			newMap.SetActive(true);
			getNewMapMap ().initialize ();
			getNewMapMap ().centerMercator = tileCenterMercator;

			getNewMapMap ().DrawMap ();

			getNewMapMap ().transform.localScale = Vector3.Scale(
				new Vector3 (getNewMapMap ().mapRectangle.getWidthMeters (), getNewMapMap ().mapRectangle.getHeightMeters (), 1.0f),
				new Vector3(getNewMapMap ().realWorldtoUnityWorldScale.x, getNewMapMap ().realWorldtoUnityWorldScale.y, 1.0f));	

			Vector2 tempPosition = GameManager.Instance.getMainMapMap ().getPositionOnMap (getNewMapMap ().centerLatLon);
			newMap.transform.position = new Vector3 (tempPosition.x, 0, tempPosition.y);

			GameObject temp = newMap;
			newMap = mainMap;
			mainMap = temp;

		}
		if(getMainMapMap().isDrawn && mainMap.GetComponent<MeshRenderer>().enabled == false){
			mainMap.GetComponent<MeshRenderer>().enabled = true;
			newMap.GetComponent<MeshRenderer>().enabled = false;
			newMap.SetActive(false);
		}
	}

	public Vector3? ScreenPointToMapPosition(Vector2 point){
		var ray = Camera.main.ScreenPointToRay(point);
		//RaycastHit hit;
		// create a plane at 0,0,0 whose normal points to +Y:
		Plane hPlane = new Plane(Vector3.up, Vector3.zero);
		float distance = 0; 
		if (!hPlane.Raycast (ray, out distance)) {
			// get the hit point:
			return null;
		}
		Vector3 location = ray.GetPoint (distance);
		return location;
	}

}
