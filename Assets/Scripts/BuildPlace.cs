using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildPlace : MonoBehaviour {
    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;
    [Header("Optional")]
    public GameObject tower;
    private Renderer rend;
    private Color startColor;
    BuildManager buildManager;
    void Start () {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
    }
    public Vector3 GetBuildPosition () {
        return transform.position + positionOffset;
    }
    void OnMouseDown () {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!buildManager.CanBuild)
            return;
        if (tower != null) {
            Debug.Log("Can't build there");
            return;
        }
        buildManager.BuildTowerOn(this);
    }
    void OnMouseEnter () {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (!buildManager.CanBuild)
            return;
        if (buildManager.HasEnoughMoney) {
            rend.material.color = hoverColor;
        } else {
            rend.material.color = notEnoughMoneyColor;
        }
    }
    void OnMouseExit() {
        rend.material.color = startColor;
    }
}
