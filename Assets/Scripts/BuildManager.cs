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
    
    private TowerBlueprint towerToBuild;
    public bool CanBuild {get {return towerToBuild != null; } }
    public void BuildTowerOn (BuildPlace buildPlace) {
        GameObject tower = (GameObject)Instantiate(towerToBuild.prefab, buildPlace.GetBuildPosition(), transform.rotation);
        buildPlace.tower = tower;
    }
    public void SelectTowerToBuild (TowerBlueprint tower) {
        towerToBuild = tower;
    }
}
