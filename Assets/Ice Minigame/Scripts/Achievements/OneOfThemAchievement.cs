using UnityEngine;
using System.Collections;

public class OneOfThemAchievement : Achievement {

	private static readonly int ID = Achievement.nextId();
	private static readonly int totalEnemies = 1000;
	private static readonly Sprite achieved = SpriteRepository.Get("red");  
	private static readonly Sprite notAchieved = SpriteRepository.Get("mysterious_ball");  

	public OneOfThemAchievement() : base (achieved, notAchieved, "One of Them", 
		"Show them that you are better than them.", 
		string.Format("Make a total of {0} enemies fall into the abyss.", totalEnemies)) {
		id = ID;
	}
		
	public override bool evaluate (GeneralStats generalStats) {
		if (generalStats.totalEnemiesDefeated >= totalEnemies) {
			return true;
		}
		return false;
	}

	public override void effect() {
		SkinsRepository.addSkin (new Skin(achieved, MaterialsRepository.Get ("red")));
	}
}
