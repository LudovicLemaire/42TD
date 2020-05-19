using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    void Awake () {
        if (instance) {
            Debug.LogError("More than one BuildManager in Scene");
            return;
        }
        instance = this;
    }
    public GameObject basicTowerPrefab;
    public GameObject siegeTowerPrefab;
    public GameObject laserTowerPrefab;
    public GameObject buildEffect;
    
    private TowerBlueprint towerToBuild;
    public bool CanBuild {get {return towerToBuild != null; } }
    public bool HasEnoughMoney {get {return PlayerStats.Money >= towerToBuild.cost; } }
    public void BuildTowerOn (BuildPlace buildPlace) {
        if (PlayerStats.Money < towerToBuild.cost) {
            Debug.Log("Too poor");
            return;
        }
        PlayerStats.Money -= towerToBuild.cost;
        GameObject tower = (GameObject)Instantiate(towerToBuild.prefab, buildPlace.GetBuildPosition(), Quaternion.identity);
        buildPlace.tower = tower;
        GameObject effect = (GameObject)Instantiate(buildEffect, buildPlace.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 3f);
    }
    public void SelectTowerToBuild (TowerBlueprint tower) {
        towerToBuild = tower;
    }
}
