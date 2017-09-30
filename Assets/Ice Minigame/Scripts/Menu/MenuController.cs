using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Play () {
		SceneManager.LoadScene ("game");
	}

	public void Achievements () {
		SceneManager.LoadScene ("achievements");
	}

	public void Customize () {
		SceneManager.LoadScene ("customize");
	}
}
