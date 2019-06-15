using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool
{
	public List<T> Create<T>(GameObject prefab, int count) 
		where T : MonoBehaviour 
	{
		// Create new list
		List<T> newPool = new List<T>();

		// Create objects
		for (int i = 0; i < count; i++) {
			GameObject projectileObject = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity);
			T projectile = projectileObject.GetComponent<T>();
			newPool.Add(projectile);
		}

		return newPool;

	}

}

public class ProjectilePool : Pool
{
	public List<Projectile> projectiles = new List<Projectile>();

	public ProjectilePool (GameObject prefab, int count) {
		projectiles = Create <Projectile>(prefab, count);
	}

	public void SetAllProjectiles () {
		foreach (Projectile projectile in projectiles)
			projectile.SetInactive ();
	}
}
