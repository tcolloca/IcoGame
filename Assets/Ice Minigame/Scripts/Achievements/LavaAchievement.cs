using UnityEngine;
using System.Collections;

public class LavaAchievement : Achievement {

	private static readonly int ID = Achievement.nextId();
	private static readonly int coins = 50;
	private static readonly Sprite achieved = SpriteRepository.Get("lava");  
	private static readonly Sprite notAchieved = SpriteRepository.Get("mysterious_ball");  

	public LavaAchievement() : base (achieved, notAchieved, "On Fire!", 
		string.Format("Make an impressive score.", coins),  
		string.Format("Collect {0} coins in one game.", coins)) {
		id = ID;
	}

	public override bool evaluate (GeneralStats generalStats) {
		if (generalStats.maxCoins >= coins) {
			return true;
		}
		return false;
	}

	public override void effect() {
		SkinsRepository.addSkin (new Skin(achieved, MaterialsRepository.Get ("lava")));
	}
}
