using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MaterialsRepository {

	public static Dictionary<string, Material> dict = new Dictionary<string, Material>();

	static MaterialsRepository() {
		DirectoryInfo dirInfo = new DirectoryInfo("Assets/Resources/Materials");
		foreach (FileInfo fileInfo in dirInfo.GetFiles()) {
			if (fileInfo.Name.EndsWith (".mat")) {
				string name = fileInfo.Name.Split ('.')[0];
				dict.Add(name, Resources.Load<Material>(string.Format ("Materials/{0}", name)));
			}
		}
	}

	public static Material Get(string name) {
		Material material = null;
		dict.TryGetValue(name, out material);
		return material;
	}
}
