using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour {
    public GameObject monsterPrefab;
     // Spawn Delay in seconds
    public float timeBetweenWaves = 10.5f;
    private float countdown = 5f;
    private int waveNb = 1;
    public Text waveCountdownText;

    void Update () {
        if (countdown <= -0.5f) {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
        waveCountdownText.text = Mathf.Round(countdown).ToString();
    }
    IEnumerator SpawnWave () {
        for (int i = 0; i < waveNb; i++) {
            SpawnMonster();
            yield return new WaitForSeconds(0.75f);
        }
        waveNb++;        
    }
    
    void SpawnMonster () {
        Instantiate(monsterPrefab, transform.position, transform.rotation);
    }
}
