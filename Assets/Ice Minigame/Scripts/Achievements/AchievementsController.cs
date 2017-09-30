using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AchievementsController : MonoBehaviour {

	public GameObject achievementModel;
	private List<Achievement> achievements; 

	// Use this for initialization
	void Start () {
		this.achievements = GeneralStats.instance.achievements;
		foreach (Achievement achievement in achievements) {
			GameObject achievementObj = Instantiate(achievementModel);
			foreach (Transform child in achievementObj.transform) {
				if(child.gameObject.tag == "AchievementImage") {
					if (achievement.isAchieved) {
						child.GetComponent<Image> ().sprite = achievement.achievedImage;
					} else {
						child.GetComponent<Image> ().sprite = achievement.notAchievedImage;
					}
				} 
				if(child.gameObject.tag == "AchievementTitle") {
					child.GetComponent<Text>().text = achievement.title;
				} else if (child.gameObject.tag == "AchievementDescription") {
					if (achievement.isAchieved) {
						child.GetComponent<Text>().text = achievement.description;
					} else {
						child.GetComponent<Text>().text = "Hint: " + achievement.hint;
					}
				}
			}
			achievementObj.transform.SetParent (transform);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void MainMenu() {
		SceneManager.LoadScene ("menu");
	}
}
