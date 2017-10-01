using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	public GameManager gameManager;
	public float speed = 1.0f;

	// Use this for initialization
	void Start () {
		enabled = SystemInfo.deviceType == DeviceType.Desktop;
	}
	
	// Update is called once per frame
	void Update () {

		gameManager.lat = gameManager.lat + Input.GetAxis("Vertical")*speed*Time.deltaTime;
		gameManager.lon = gameManager.lon + Input.GetAxis("Horizontal")*speed*Time.deltaTime;
	}
}
