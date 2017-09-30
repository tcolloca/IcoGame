using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public static float speed = 2f;

	private GameStats gameStats;
	private string playerTag = "Player";
	private string gameControllerTag = "GameController";
	private GameObject player;
	private Rigidbody rigidBody;
    private Vector3 playerPosition;
    private Vector3 direction;

    // Use this for initialization
    void Start () {
		player = GameObject.FindGameObjectWithTag (playerTag);
		rigidBody = GetComponent<Rigidbody> ();
        playerPosition = player.transform.position;
		gameStats = GameObject.FindGameObjectWithTag (gameControllerTag).GetComponent<GameController>().gameStats;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y < -10)
        {
			gameStats.enemyDefeated ();
            gameObject.SetActive(false);
            return;
        }

        float velY = rigidBody.velocity.y;
        rigidBody.velocity = direction * speed + new Vector3(0, velY, 0);
    }

    public void SetPlayerPosition(Vector3 pos)
    {
        playerPosition = pos;
        direction = playerPosition - transform.position;
        direction.y = 0;
        direction.Normalize();
    }
}
