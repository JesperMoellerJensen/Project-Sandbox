using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
	Rigidbody body;
	public float Thrust = 0f;

	void FixedUpdate()
	{
		// TODO: Rigitbody should not be set every update!!!
		body = transform.root.GetComponent<Rigidbody>();


		body.AddForceAtPosition(transform.forward * Thrust * Time.deltaTime, transform.position);
	}
}
