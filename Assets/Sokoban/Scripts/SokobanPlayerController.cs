using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SokobanPlayerController : MonoBehaviour {

	private Direction direction = Direction.DOWN;

	public Board board { private get; set; }
	public float scale { private get; set; }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Direction newDir = Direction.NONE;
		if (Input.GetKeyDown (KeyCode.A) || Input.GetKeyDown (KeyCode.LeftArrow)) {
			newDir = Direction.LEFT;
		} else if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)) {
			newDir = Direction.UP;
		} else if (Input.GetKeyDown (KeyCode.D) || Input.GetKeyDown (KeyCode.RightArrow)) {
			newDir = Direction.RIGHT;
		} else if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow)) {
			newDir = Direction.DOWN;
		}
		if (!newDir.Equals (Direction.NONE) && board.Move (newDir)) {
			transform.Translate (newDir.GetTranslation () * scale);
			direction = newDir;
		}
	}
}
