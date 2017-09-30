using System;
using UnityEngine;

public class Skin {

	public Sprite image { get; private set; }
	public Material material { get; private set; }

	public Skin(Sprite image, Material material) {
		this.image = image;
		this.material = material;
	}
}

