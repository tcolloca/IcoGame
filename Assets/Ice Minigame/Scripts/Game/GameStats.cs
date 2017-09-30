using UnityEngine;
using System.Collections;

public class GameStats {

	public int coins { get; private set;}
	public int enemiesDefeated { get; private set;}
	public float time { get; set;}
	private bool isGameOver = false;

	public void addCoin() {
		coins++;
	}

	public void enemyDefeated() {
		enemiesDefeated++;
	}

	public void addTime() {
		if (!isGameOver) {
			time += Time.deltaTime;
		}
	}

	public void gameOver() {
		isGameOver = true;
	}
}
