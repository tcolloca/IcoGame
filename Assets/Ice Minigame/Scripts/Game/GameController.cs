using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public int playerLives;
	public Text bestScoreText;

	public GameStats gameStats = new GameStats();
    private Text scoreText;
    private Text livesText;

    // Use this for initialization
    void Start () {
		GeneralStats.instance.gamesPlayed++;
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        livesText = GameObject.FindGameObjectWithTag("Lives").GetComponent<Text>();
		scoreText.text = gameStats.coins + "";
        livesText.text = playerLives + "";
		bestScoreText.text = "Best Score: " + GeneralStats.instance.maxCoins;
        EnemyController.speed = 2f;
    }
	
	// Update is called once per frame
	void Update () {
		gameStats.addTime ();

		if (Input.GetKeyDown(KeyCode.Q)) {
			GameOver ();
		}
	}

    public void CoinPicked()
    {
		gameStats.addCoin ();
		if (gameStats.coins > GeneralStats.instance.maxCoins) {
			bestScoreText.text = "Best Score: " + gameStats.coins;
		}
		scoreText.text = gameStats.coins + "";
    }

    public void PlayerDied()
    {
        playerLives--;
        livesText.text = playerLives + "";
        if (playerLives <= 0)
        {
            GameOver();
            return;
        }
    }

    private void GameOver()
    {
		gameStats.gameOver ();
		GeneralStats.instance.lastGameStats = gameStats;
		SceneManager.LoadScene ("gameOver");
        //TODO: go to after-game scene or return to menu
    }
}
