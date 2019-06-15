using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
	public Color flashDamageColor = Color.white;

	private MeshRenderer mesh = null;
	private Color originalColor = Color.white;

	private int maxHealth = 2;
	private int health = 0;


	private void Awake () {
		mesh = GetComponent<MeshRenderer> ();
		originalColor = mesh.material.color;
	}

	private void OnEnable () {
		ResetHealth ();
	}

	private void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.CompareTag ("Projectile")) {
			Damage ();
		}
	}

	private void Damage(){
		StopAllCoroutines ();
		StartCoroutine (Flash());
		RemoveHealth ();
	}

	private IEnumerator Flash () {
		mesh.material.color = flashDamageColor;

		WaitForSeconds wait = new WaitForSeconds (0.1f);
		yield return wait;

		mesh.material.color = originalColor;
	}

	private void RemoveHealth (){
		health--;
		CheckForDeath ();
	}

	private void ResetHealth () {
		health = maxHealth;
	}

	private void CheckForDeath (){
		if (health <= 0)
			Kill ();
	}

	private void Kill () {
		gameObject.SetActive (false);
	}


}
