using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkinsRepository {

	public static List<Skin> skins { get; private set; }

	static SkinsRepository() {
		skins = new List<Skin> ();
	}

	public static void addSkin(Skin skin) {
		skins.Add (skin);
	}
}
