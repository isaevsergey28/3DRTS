using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildingFactory
{
    void Load();
    GameObject Create(BuildingType buildType, Vector3 at);
}
