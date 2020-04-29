using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerV2 : MonoBehaviour {
	private Transform target;
	[Header("Attributes")]
	public float range = 5f;
	public float turnSpeed = 5f;
	public float fireRate = 1f;
	private float fireCountDown = 0f;
	[Header("Setup")]
	public string enemyTag = "Enemy";
	public Transform partToRotate;
	public GameObject projectilePrefab;
	public Transform firePoint;
	
	// Start is called before the first frame update
	void Start() {
		InvokeRepeating ("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget () {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		float shortestDist = Mathf.Infinity;
		GameObject nearestEnemy = null;
		foreach (GameObject enemy in enemies) {
			float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
			if (distanceToEnemy < shortestDist) {
				shortestDist = distanceToEnemy;
				nearestEnemy = enemy;
			}
		}
		if (nearestEnemy != null && shortestDist <= range) {
			target = nearestEnemy.transform;
		} else {
			target = null;
		}
	}

	// Update is called once per frame
	void Update() {
		if (target == null)
			return;
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
		
		if (fireCountDown <= 0f) {
			Shoot();
			fireCountDown = 1f / fireRate;
		}
		fireCountDown -= Time.deltaTime;
	}
	void Shoot () {
		GameObject projectileGO = (GameObject)Instantiate (projectilePrefab, firePoint.position, firePoint.rotation);
		Projectile projectile = projectileGO.GetComponent<Projectile>();
		if (projectile != null)
			projectile.Seek(target);
	}
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
