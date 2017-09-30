using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.IO;

public class SpriteRepository {
     
	public static Dictionary<string, Sprite> dict = new Dictionary<string, Sprite>();
 
	static SpriteRepository() {
		DirectoryInfo dirInfo = new DirectoryInfo("Assets/Ice Minigame/Resources/Sprites");
		foreach (FileInfo fileInfo in dirInfo.GetFiles()) {
			if (!fileInfo.Name.EndsWith (".meta")) {
				string name = fileInfo.Name.Split ('.')[0];
				dict.Add(name, Resources.Load<Sprite>(string.Format ("Sprites/{0}", name)));
			}
		}
	}

	public static Sprite Get(string name) {
		Sprite sprite = null;
		dict.TryGetValue(name, out sprite);
		return sprite;
     }
 }