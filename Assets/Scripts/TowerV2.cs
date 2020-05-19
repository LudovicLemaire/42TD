using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerV2 : MonoBehaviour {
	private Transform target;
	private Monster targetEnemy;
	private GameObject[] multiEnemies;
	public string enemyTag = "Enemy";

	[Header("General")]
	public float range = 5f;
	public float turnSpeed = 5f;
	[Header("Use Projectile (default)")]
	public GameObject projectilePrefab;
	public float fireRate = 1f;
	private float fireCountDown = 0f;
	public bool isMulti = false;

	[Header("Use Laser")]
	public bool useLaser = false;
	public float damageOverTime = 5f;
	public float slowPercent = 0;
	public LineRenderer lineRenderer;
	public ParticleSystem impactEffect;
	public Light impactLight;
	public ParticleSystem impactEffectCannon;
	public Light impactLightCannon;
	

	[Header("Setup")]
	public Transform partToRotate;
	public Transform firePoint;
	
	// Start is called before the first frame update
	void Start() {
		InvokeRepeating ("UpdateTarget", 0f, 0.5f);
	}

	void UpdateTarget () {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
		if (isMulti)
			multiEnemies = enemies;
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
			targetEnemy = nearestEnemy.GetComponent<Monster>();
		} else {
			target = null;
		}
	}

	// Update is called once per frame
	void Update() {
		if (target == null) {
			if (useLaser)
				if (lineRenderer.enabled) {
					lineRenderer.enabled = false;
					impactLight.enabled = false;
					impactEffect.Stop();
					impactLightCannon.enabled = false;
					impactEffectCannon.Stop();
				}
			return;
		}
		if (!isMulti)
			LockOnTarget();
		else
			partToRotate.transform.Rotate(Vector3.up * Time.deltaTime * 70, Space.World);
		if (useLaser) {
			Laser();
		} else {
			if (fireCountDown <= 0f) {
				if (isMulti)
					ShootMulti();
				else
					Shoot();
				fireCountDown = 1f / fireRate;
			}
			fireCountDown -= Time.deltaTime;
		}
		
	}
	void Laser () {
		targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
		if (slowPercent > 0) {
			targetEnemy.Slow(slowPercent);
		}
		if (!lineRenderer.enabled) {
			lineRenderer.enabled = true;
			impactLight.enabled = true;
			impactEffect.Play();
			impactLightCannon.enabled = true;
			impactEffectCannon.Play();
		}
		lineRenderer.SetPosition(0, firePoint.position);
		lineRenderer.SetPosition(1, target.position);

		Vector3 dir = firePoint.position - target.position;
		impactEffect.transform.position = target.position + dir.normalized * 0.75f;
		impactEffect.transform.rotation = Quaternion.LookRotation(dir);
	}
	void LockOnTarget() {
		Vector3 dir = target.position - transform.position;
		Quaternion lookRotation = Quaternion.LookRotation(dir);
		Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
		partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
	}
	void Shoot () {
		GameObject projectileGO = (GameObject)Instantiate (projectilePrefab, firePoint.position, firePoint.rotation);
		Projectile projectile = projectileGO.GetComponent<Projectile>();
		if (projectile != null)
			projectile.Seek(target);
	}
	void ShootMulti () {
		foreach (GameObject enemy in multiEnemies) {
			float distanceToEnemy = Vector3.Distance (transform.position, enemy.transform.position);
			if (distanceToEnemy <= range) {
				GameObject projectileGO = (GameObject)Instantiate (projectilePrefab, firePoint.position, firePoint.rotation);
				Projectile projectile = projectileGO.GetComponent<Projectile>();
				if (projectile != null)
					projectile.Seek(enemy.transform);
			}
		}
		
	}
	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
}
