using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blaster : MonoBehaviour
{

	// TODO SteamVR - move the Blaster to be child of right controller, 
	// adjust transform of Barrel accordingly
	//	[Header("Input")]
	// public SteamVR_Action_Boolean fireAction = null;
	// public SteamVR_Action_Boolean reloadAction = null;
	// private SteamVR_Behaviour_Pose pose = null;

	[Header("Settings")]
	public int force = 50;
	public int maxProjectileCount = 6;
	public float reloadTime = 1.5f;

	[Header("References")]
	public GameObject prefab = null;
	public Transform barrel = null;
	public Text ammoOutput = null;

	private bool isReloading = false;
	private int fireCount = 0;

	private Animator animator = null;
	private ProjectilePool projectilePool = null;


	private void Awake() {
		// TODO SteamVR
		// pose = GetComponentInParent<Steam_Behaviour_Pose>();

		projectilePool = new ProjectilePool (prefab, maxProjectileCount);
		animator = GetComponent<Animator> ();
	}

	private void Start(){
		UpdateFireCount (0);
	}

	private void Update () {
		if (isReloading)
			return;
		
		// TODO SteamVR
		// if (fireAction.GetStateDown(pose.inputSource))
		if (GvrPointerInputModule.Pointer.TriggerDown || Input.GetKeyDown ("space")) {
			animator.SetBool ("Fire", true);
			Fire ();
		}
		if (GvrPointerInputModule.Pointer.TriggerUp || Input.GetKeyUp ("space")) {
			animator.SetBool ("Fire", false);
		}

		// TODO SteamVR
		// if (reloadAction.GetStateDown(pose.inputSource))
		if (Input.GetKeyDown ("r")) {
			StartCoroutine (Reload ());
		}

	}

	private void Fire(){
		if (fireCount >= maxProjectileCount)
			return;
		
		Projectile targetProjectile = projectilePool.projectiles [fireCount];
		targetProjectile.Launch (this);
		UpdateFireCount (fireCount + 1);

	}

	private IEnumerator Reload(){
		if (fireCount == 0)
			yield break;

		ammoOutput.text = "-";
		isReloading = true;
		projectilePool.SetAllProjectiles ();

		yield return new WaitForSeconds (reloadTime);
		UpdateFireCount(0);
		isReloading = false;

	}

	private void UpdateFireCount (int newValue){
		fireCount = newValue;
		ammoOutput.text = (maxProjectileCount - fireCount).ToString();

	}
}
