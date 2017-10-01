using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public const string LOG_TAG = "MY_FANCY_LOG: ";

    public float speed = 25f;
    public float godmodeDuration = 5f;

    public GameController gameController;

    private Rigidbody rigidBody;
    private LineRenderer lr;

    private float timeSinceRespawn;

    private MeshRenderer godmodeRenderer;

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody>();
        lr = GameObject.FindGameObjectWithTag("Line").GetComponent<LineRenderer>();
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Enemies"), true);
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("CenterObstacle"), true);
        godmodeRenderer = GameObject.FindGameObjectWithTag("GodmodeSphere").GetComponent<MeshRenderer>();
//		GetComponent<MeshRenderer> ().material = GeneralStats.instance.ballMaterial;
		Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Player"), false);
    }
	
    void OnTriggerEnter(Collider collider)
    {
        Rigidbody attachedRigidbody = collider.attachedRigidbody;
        string tag = attachedRigidbody.gameObject.tag;
        if (tag.Equals("Coin"))
        {
            attachedRigidbody.gameObject.SetActive(false);
            gameController.CoinPicked();
        }
    }

	// Update is called once per frame
	void Update () {
        lr.SetPosition(0, new Vector3(0, transform.position.y, 0));
        lr.SetPosition(1, transform.position);

        if (transform.position.y < -10)
        {
            Die();
            return;
        }

		HandleInput ();

        if (godmodeRenderer.enabled && Time.realtimeSinceStartup - timeSinceRespawn >= godmodeDuration)
        {
            godmodeRenderer.enabled = false;
            Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Player"), false);
        }
    }

	public void HandleInput() {
		Vector2 input = SystemInfo.deviceType==DeviceType.Handheld?GetMobileInput():GetDesktopInput();

		float horizontal = input.x;
		float vertical = input.y;

		Vector3 toCenter = -rigidBody.position;
		float yVel = rigidBody.velocity.y;
		toCenter.y = 0;
		toCenter = toCenter.normalized;

		rigidBody.velocity = toCenter * speed * vertical * Time.deltaTime + new Vector3(0, yVel, 0);

		Vector3 tangent = Quaternion.Euler(0, 90, 0) * toCenter;
		rigidBody.velocity += tangent * speed * horizontal * Time.deltaTime;
	}

	public Vector2 GetDesktopInput() {
		return new Vector2 (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	}

	public Vector2 GetMobileInput() {

		float horizontal = 0;
		float vertical = 0;

//		if (Input.touchCount > 0)
//		{
//
////			//Store the first touch detected.
//			Touch touch = Input.touches[0];
//			Debug.Log (touch.position);
//
//			if (touch.position.x < Screen.width * 0.2) {
//				//going left
//				Debug.Log (LOG_TAG + "going left");
//				horizontal = -1;
//			}
//
//			if (touch.position.x > Screen.width * 0.8) {
//				//going right
//				Debug.Log (LOG_TAG + "going right");
//				horizontal = 1;
//			}
//		}

		Debug.Log (LOG_TAG + Input.acceleration.ToString());

		if (Input.acceleration.y > -0.3) {
			//going forward
			Debug.Log (LOG_TAG + "going forward");
			vertical = 1;
		}

		if (Input.acceleration.y < -0.7) {
			//going backwards
			Debug.Log (LOG_TAG + "going backwards");
			vertical = -1;
		}

		if (Input.acceleration.x > 0.1) {
			//going right
			Debug.Log (LOG_TAG + "going right");
			horizontal = 1;
		}

		if (Input.acceleration.x < -0.1) {
			//going left
			Debug.Log (LOG_TAG + "going left");
			horizontal = -1;
		}

		return new Vector2 (horizontal, vertical);
	}

    public void Die()
    {
        Respawn();
        gameController.PlayerDied();
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Player"), true);
        godmodeRenderer.enabled = true;
    }

    private void Respawn()
    {
        transform.position = new Vector3(6, 2, 0);
        timeSinceRespawn = Time.realtimeSinceStartup;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag.Equals("Enemy"))
        {
            collision.collider.attachedRigidbody.gameObject.SetActive(false);
            Die();
        }
    }
}
