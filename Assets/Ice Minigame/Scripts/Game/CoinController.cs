using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {

    private Rigidbody myRigidBody;
    private float rotationSpeed = 180f;
    private float times = 0;

	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody>();

    }
	
	// Update is called once per frame
	void Update () {
        myRigidBody.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        Vector3 newPosition = myRigidBody.position;
        newPosition.y = (float)1.5 + 0.075f*Mathf.Sin(times/13f);
        times++;
        myRigidBody.position = newPosition;
    }
}
