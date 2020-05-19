using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour {
    public GameObject monsterPrefab;
     // Spawn Delay in seconds
    public float timeBetweenWaves = 10f;
    public float countdown = 5f;
    private int waveNb = 1;
    public Text waveCountdownText;
    public Text waveIndex;

    void Update () {
        if (countdown <= 0) {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }
    IEnumerator SpawnWave () {
        waveNb++;
        waveIndex.text = (waveNb - 1).ToString();
        for (int i = 0; i < waveNb - 1; i++) {
            SpawnMonster();
            yield return new WaitForSeconds(0.75f);
        }
        
    }
    
    void SpawnMonster () {
        Instantiate(monsterPrefab, transform.position, transform.rotation);
    }
}
