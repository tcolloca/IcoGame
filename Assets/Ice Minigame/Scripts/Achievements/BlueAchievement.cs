using UnityEngine;
using System.Collections;

public class BlueAchievement : Achievement {

	private static readonly int ID = Achievement.nextId();
	private static readonly int time = 220;
	private static readonly Sprite achieved = SpriteRepository.Get("blue");  
	private static readonly Sprite notAchieved = SpriteRepository.Get("mysterious_ball");  

	public BlueAchievement() : base (achieved, notAchieved, "I'm Blue", 
		"Survive as long as the song lasts.", 
		string.Format("Survive for {0} seconds.", time)) {
		id = ID;
	}

	public override bool evaluate (GeneralStats generalStats) {
		if (generalStats.lastGameStats.time >= time) {
			return true;
		}
		return false;
	}

	public override void effect() {
		SkinsRepository.addSkin (new Skin(achieved, MaterialsRepository.Get ("blue")));
	}
}
