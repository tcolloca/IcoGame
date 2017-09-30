using UnityEngine;
using System.Collections;

public abstract class Achievement {

	private static int lastId = 0;

	public int id { get; set; }
	public Sprite achievedImage { get; private set; }
	public Sprite notAchievedImage { get; private set; }
	public string title { get; private set; }
	public string hint { get; private set; }
	public string description { get; private set; }
	public bool isAchieved { get; private set; }
	
	public Achievement (Sprite achievedImage, Sprite notAchievedImage, string title, string hint, string description) {
		this.achievedImage = achievedImage;
		this.notAchievedImage = notAchievedImage;
		this.title = title;
		this.hint = hint;
		this.description = description;
		this.isAchieved = false;
	}

	public abstract bool evaluate (GeneralStats generalStats);

	public void achieve() {
		isAchieved = true;
		effect ();
	}

	public abstract void effect ();

	public static int nextId() {
		return lastId++;
	}
}
