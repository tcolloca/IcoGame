using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUpdateController : MonoBehaviour {

	public string sceneName;

	private Collider targetCollider;

	// Use this for initialization
	void Start () {
		this.targetCollider = gameObject.GetComponent<Collider> ();
		if (targetCollider == null) {
			throw new MissingComponentException ("Missing Collider component.");
		}
		Debug.Log (sceneName);
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 position = Vector2.zero;
		Debug.Log (Input.touchCount);
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Began) {
				position = touch.position;
			}
		}
		if (Input.GetMouseButtonDown (0)) {
			position = Input.mousePosition;
		}
		if (position != Vector2.zero) {
			Ray ray = Camera.main.ScreenPointToRay (position);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider != null) {
					if(hit.collider.Equals(targetCollider)) {
						SceneManager.LoadScene (sceneName);
					}
				}
			}
		}
	}
}
