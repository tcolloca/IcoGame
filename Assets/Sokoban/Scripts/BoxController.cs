using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour {

	public float scale { private get; set; }

	public void Move (Direction dir) {
		transform.Translate (dir.GetTranslation () * scale);
	}
}
