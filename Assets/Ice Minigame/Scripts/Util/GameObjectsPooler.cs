using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectsPooler : MonoBehaviour {

	public static GameObjectsPooler instance;

	public int poolSize = 10;
	public bool resizable = true;
	public GameObject poolObject;

	private List<GameObject> pool = new List<GameObject>();

	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		for (int i = 0; i < poolSize; i++) {
			addObject ();
		}
	}
	
	public GameObject getObject() {
		foreach (GameObject obj in pool) {
			if (!obj.activeInHierarchy) {
				return obj;
			}
		}
		if (resizable) {
			return addObject ();
		}
		return null;
	}

	GameObject addObject() {
		GameObject obj = Instantiate (poolObject);
		obj.SetActive (false);
		pool.Add (obj);
		return obj;
	}
}
