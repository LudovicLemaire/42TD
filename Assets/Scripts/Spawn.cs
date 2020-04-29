using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
    public GameObject monsterPrefab;
     // Spawn Delay in seconds
    public float interval = 3;

    // Start is called before the first frame update
    void Start() {
        InvokeRepeating("SpawnNext", interval, interval);
    }

    void SpawnNext() {
        Instantiate(monsterPrefab, transform.position, Quaternion.identity);
    }
}
