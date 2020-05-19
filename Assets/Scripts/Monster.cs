using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    public float health = 100f;
    public int moneyWorth = 50;
    public float startSpeed = 3f;
    [HideInInspector]
    public float speed;
    public GameObject deathEffect;
    public GameObject subTier;
    private Renderer rend;
    private Color startColor;
    public Color slowColor;
    public float countdownFreeze = 3f;
    private float currentCountdownFreeze;
    private bool didSubtier = false;

    void Start() {
        speed = startSpeed;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        currentCountdownFreeze = countdownFreeze;
    }

    void Update() {
        currentCountdownFreeze -= Time.deltaTime;
        if (currentCountdownFreeze <= 0) {
            speed = startSpeed;
            rend.material.color = startColor;
        }
    }
    public void TakeDamage (float amount) {
        health -= amount;
        if (health <= 0) {
            Die();
        }
    }
    public void Slow (float slowPercent) {
        speed = startSpeed * (1f - slowPercent);
        rend.material.color = slowColor;
        currentCountdownFreeze = countdownFreeze;
    }
    void OnTriggerEnter(Collider co) {
        // If castle then deal Damage, destroy self
        if (co.name == "Castle") {
            PlayerStats.Lives--;
            Destroy(gameObject);
        }
    }
    void Die () {
        PlayerStats.Money += moneyWorth;
        GameObject effectIns =  (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effectIns, 3f);
        if (subTier && !didSubtier) {
            didSubtier = true;
            Instantiate(subTier, transform.position, transform.rotation);
            Instantiate(subTier, transform.position, transform.rotation);
        }
        Destroy(gameObject);
    }
}