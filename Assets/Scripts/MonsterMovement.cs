using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Monster))]
public class MonsterMovement : MonoBehaviour
{
    private Monster monster;
    private NavMeshAgent navMeshAgent;
    void Start () {
        monster = GetComponent<Monster>();
        // Navigate to Castle
        GameObject castle = GameObject.Find("Castle");
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (castle)
            navMeshAgent.destination = castle.transform.position;
    }
    void Update () {
        navMeshAgent.speed = monster.speed;
    }
}
