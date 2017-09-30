using UnityEngine;
using System.Collections;

public class CamerasController : MonoBehaviour {

    private GameObject[] cameras;
    private int activeCamera = 0;

	// Use this for initialization
	void Start () {
        cameras = GameObject.FindGameObjectsWithTag("Camera");
        for (int i = 1; i < cameras.Length; i++)
        {
            cameras[i].SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        bool key = Input.GetKeyDown(KeyCode.T);
        if (key)
        {
            cameras[activeCamera].SetActive(false);
            activeCamera = (activeCamera + 1) % cameras.Length;
            cameras[activeCamera].SetActive(true);
        }
	}
}
