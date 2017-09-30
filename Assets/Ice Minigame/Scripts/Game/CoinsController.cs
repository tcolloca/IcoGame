using UnityEngine;
using System.Collections;

public class CoinsController : MonoBehaviour {

	public float spawnFrequency = 3;
	public GameObjectsPooler coinsPooler; 

	private float time = 0f;
	private float lastCreationTime = 0;
	private string boardTag = "Board";
	private float boardWidth;
    private GameObject board;

	// Use this for initialization
	void Start () {
		board = GameObject.FindGameObjectWithTag (boardTag);
		Bounds boardBounds = board.GetComponent<MeshFilter> ().mesh.bounds;
		boardWidth = board.transform.localScale.x * boardBounds.size.x * 0.5f;
	}

	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		lastCreationTime += Time.deltaTime;
		if ((int)time % spawnFrequency == 0 && lastCreationTime > spawnFrequency) {
			lastCreationTime = 0;
			GameObject coin = coinsPooler.getObject ();
            if (coin == null) return;
            float yPos = 1.5f;
            float xPos = Random.Range(-boardWidth, boardWidth);
			float zPos = Random.Range(-boardWidth, boardWidth);

            coin.transform.position = new Vector3(xPos, yPos, zPos);
			coin.SetActive (true);
		}
	}
}
