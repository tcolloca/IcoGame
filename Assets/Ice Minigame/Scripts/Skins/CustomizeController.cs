using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class CustomizeController : MonoBehaviour {

	public GameObject ball;
	public GameObject skinModel;

	// Use this for initialization
	void Start () {
		List<Skin> skins = SkinsRepository.skins;
		foreach (Skin skin in skins) {
			GameObject skinObj = Instantiate(skinModel);
			foreach (Transform child in skinObj.transform) {
				if (child.gameObject.tag == "BallSkinImage") {
					child.GetComponent<Image> ().sprite = skin.image;
				} 
			}
			Material material = skin.material;
			skinObj.GetComponent<Button> ().onClick.AddListener (() => setMaterial(material));
			skinObj.transform.SetParent (transform);
		}
	}

	private void setMaterial(Material material) {
		ball.GetComponent<MeshRenderer> ().material = material;
		GeneralStats.instance.ballMaterial = material;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void MainMenu() {
		SceneManager.LoadScene ("menu");  
	}
}
