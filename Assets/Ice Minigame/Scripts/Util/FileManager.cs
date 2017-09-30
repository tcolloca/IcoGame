using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class FileManager : MonoBehaviour {

	public static FileManager instance;

	private const string fileName = "game.stats";
	private int gamesPlayed;
	private int maxScore;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
			LoadSaveFile ();
			DontDestroyOnLoad (this.gameObject);
		}
	}

	void LoadSaveFile () {
		if (File.Exists (fileName)) {
			StreamReader reader = new StreamReader (fileName, Encoding.Default);
			string line;
			using (reader) {
				int lineNum = 0;
				GeneralStats generalStats = GeneralStats.instance;
				while ((line = reader.ReadLine ()) != null) {
					if (lineNum == 0) {
						string[] values = line.Split (';');
						if (values.Length >= 1) {
							generalStats.gamesPlayed = int.Parse (values [0]);
						}
						if (values.Length >= 2) {
							generalStats.maxCoins = int.Parse (values [1]);
						}
						if (values.Length >= 3) {
							generalStats.totalCoins = int.Parse (values [2]);
						}
						if (values.Length >= 4) {
							generalStats.maxTime = int.Parse (values [3]);
						}
						if (values.Length >= 5) {
							generalStats.totalTime = int.Parse (values [4]);
						}
						if (values.Length >= 6) {
							generalStats.maxEnemiesDefeated = int.Parse (values [5]);
						}
						if (values.Length >= 7) {
							generalStats.totalEnemiesDefeated = int.Parse (values [6]);
						}
					} else if (lineNum == 1) {
						string[] values = line.Split (';');
						for (int i = 0; values.Length >= (i + 1); i++) {
							if (bool.Parse (values [i])) {
								generalStats.getAchievement (i).achieve ();
							}
						}
					}
					lineNum++;
				}
			}
		} else {
			File.Create (fileName);
		}
	}

	public void SaveFile() {
		StreamWriter file = new System.IO.StreamWriter(File.Create(fileName));
		GeneralStats stats = GeneralStats.instance;
		string content = string.Format ("{0};{1};{2};{3};{4};{5};{6}\n", 
			                   stats.gamesPlayed, stats.maxCoins, 
							   stats.totalCoins, 
							   stats.maxTime, stats.totalTime,
			                   stats.maxEnemiesDefeated, stats.totalEnemiesDefeated);
		

		int i;
		for (i = 0; i < stats.achievements.Count - 1; i++) {
			content += stats.getAchievement (i).isAchieved + ";";
		}
		content += stats.getAchievement (i).isAchieved;
		file.Write(content);

		file.Close();
	}
}
