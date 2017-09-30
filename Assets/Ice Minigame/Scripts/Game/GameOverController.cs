using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameOverController : MonoBehaviour {

	public Text score;
	public Text time;
	public Text enemiesDefeated;
	public Text bestScore;
	public Text achievementsList;

	// Use this for initialization
	void Start () {
		score.text = GeneralStats.instance.lastGameStats.coins.ToString();
		time.text = (Mathf.RoundToInt (GeneralStats.instance.lastGameStats.time)).ToString () + "s";
		enemiesDefeated.text = GeneralStats.instance.lastGameStats.enemiesDefeated.ToString();
		bestScore.gameObject.SetActive (GeneralStats.instance.isBestScore);

		List<Achievement> unlocked = GeneralStats.instance.evaluateAchievements ();
		if (unlocked.Count == 0) {
			achievementsList.text = "-";
		} else {
			achievementsList.text = "";
			foreach (Achievement achievement in unlocked) {
				achievementsList.text += achievement.title + "\n";
			}
		}
		FileManager.instance.SaveFile ();
	}
	
	// Update is called once per frame
	void Update () {
	}
		
	public void PlayAgain() {
		SceneManager.LoadScene ("game");
	}

	public void MainMenu() {
		SceneManager.LoadScene ("menu");
	}
}
