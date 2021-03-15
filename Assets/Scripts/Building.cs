using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] BuildingType buildingType;

    public bool isRecruitment { get; set; } = false;

    public BuildingType GetBuildingType()
    {
        return buildingType;
    }


}
