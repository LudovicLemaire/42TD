using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    private Transform target;
    public float speed = 5f;
    public float damage = 10f;
    public float explosionRadius = 0f;
    public float slowPercent = 0f;
    public GameObject impactEffect;
    
    public void Seek (Transform _target) {
        target = _target;
    }
    // Update is called once per frame
    void Update() {
        if (target == null) {
            if (explosionRadius > 0f) {
                HitTarget();

            }
            else
                Destroy(gameObject);
            return ;
        }
        Vector3 dir = target.position - transform.position;
        float distThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distThisFrame) {
            HitTarget();
            return ;
        }
        transform.Translate(dir.normalized * distThisFrame, Space.World);
        transform.LookAt(target);
    }
    void HitTarget() {
        GameObject effectIns =  (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectIns, 3f);
        if (explosionRadius > 0f) {
            Explode();
        } else {
            Damage(target);
        }
        Destroy(gameObject);
    }

    void Explode() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders) {
            if (collider.tag == "Enemy") {
                Damage(collider.transform);
            }
        }
    }

    void Damage (Transform enemy) {
        Monster e = enemy.GetComponent<Monster>();
        if (e) {
            if (slowPercent > 0)
                e.Slow(slowPercent);
            e.TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
