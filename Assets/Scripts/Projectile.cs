using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float lifetime = 5f;
	private Rigidbody body;


	private void Awake() {
		body = GetComponent<Rigidbody> ();
		SetInactive ();
	}

	private void OnCollisionEnter(Collision collision){
		SetInactive ();
	}

	public void Launch(Blaster blaster) {
		// Position
		transform.position = blaster.barrel.position;
		// TODO SteamVR update barrel rotation to match properly
		transform.rotation = Camera.main.transform.rotation;
		// Activate
		gameObject.SetActive(true);

		// Fire
		body.AddForce(Camera.main.transform.forward * blaster.force, ForceMode.Impulse);

		// Track Lifetime
		StartCoroutine(TrackLifetime());

	}

	private IEnumerator TrackLifetime(){

		yield return new WaitForSeconds (lifetime);
		SetInactive ();

	}

	public void SetInactive(){

		body.velocity = Vector3.zero;
		body.angularVelocity = Vector3.zero;
		gameObject.SetActive(false);

	}
}
