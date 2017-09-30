using UnityEngine;
using System.Collections;

public class OrangeAchievement : Achievement {

	private static readonly int ID = Achievement.nextId();
	private static readonly int totalCoins = 100;
	private static readonly Sprite achieved = SpriteRepository.Get("orange");  
	private static readonly Sprite notAchieved = SpriteRepository.Get("mysterious_ball");  

	public OrangeAchievement() : base (achieved, notAchieved, "Orange Is the New Green", 
		string.Format("Collect a total of {0} coins.", totalCoins), 
		string.Format("Collect a total of {0} coins.", totalCoins)) {
		id = ID;
	}
		
	public override bool evaluate (GeneralStats generalStats) {
		if (generalStats.totalCoins >= totalCoins) {
			return true;
		}
		return false;
	}

	public override void effect() {
		SkinsRepository.addSkin (new Skin(achieved, MaterialsRepository.Get ("orange")));
	}
}
