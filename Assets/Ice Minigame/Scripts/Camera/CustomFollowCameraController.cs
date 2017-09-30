using UnityEngine;
using System.Collections;

public class CustomFollowCameraController : MonoBehaviour {

    private Camera camera;
    private GameObject player;

    // Use this for initialization
    void Start () {
        camera = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 direction = player.transform.position;
        direction.y = 0;
        direction.Normalize();
        Vector3 playerPosition = player.transform.position;
        playerPosition.y = 0;

        Vector3 newPosition = playerPosition + 15 * direction;
        newPosition.y = camera.transform.position.y;

        Vector2 normalDirection = new Vector2(1, 0);
        Vector3 newAngles;
        Vector3 previousRotation = camera.transform.eulerAngles;
        if (player.transform.position.z >= 0)
        {
            Vector2 otherDirection = new Vector2(playerPosition.x, playerPosition.z);
            float beta = Vector2.Angle(normalDirection, otherDirection);
            float betap = (360 - beta) - 90;
            newAngles = new Vector3(previousRotation.x, betap, previousRotation.z);
        } else
        {
            Vector2 otherDirection = new Vector2(playerPosition.x, playerPosition.z);
            float beta = Vector2.Angle(normalDirection, otherDirection);
            beta = beta - 90;
            newAngles = new Vector3(previousRotation.x, beta, previousRotation.z);
        }

        camera.transform.position = newPosition;
        camera.transform.rotation = Quaternion.Euler(newAngles);
    }

    private Vector3 interpolate(Vector3 begin, Vector3 end, float speed)
    {
        Vector3 direction = end - begin;
        return begin + direction * speed;
    }
}
