using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuildingPanelSystem : MonoBehaviour
{
    private Ray _ray;
    private RaycastHit _hit;
    [SerializeField] private List<GameObject> _allBuildings;
    
   
    private void Update()
    {
        if(!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
            {

                _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(_ray, out _hit))
                {
                    foreach (GameObject build in _allBuildings)
                    {
                        if (build.TryGetComponent(out Building _build))
                        {
                            GameObject panel = build.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject;

                            if (!_build.isRecruitment)
                            {
                                build.transform.GetChild(0).gameObject.GetComponent<Canvas>().enabled = true;
                                panel.SetActive(false);
                            }
                            else
                            {
                                build.transform.GetChild(0).gameObject.GetComponent<Canvas>().enabled = false;
                            }
                        }
                    }
                    if (_hit.collider.gameObject.TryGetComponent(out Building building))
                    {
                        building.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.SetActive(true);
                        building.transform.GetChild(0).gameObject.GetComponent<Canvas>().enabled = true;
                    }
                }
            }
        }
    }
    public void AddBuilding(GameObject building)
    {
        _allBuildings.Add(building);
    }
    public List<GameObject> GetAllBuildings()
    {
        return _allBuildings;
    }
    public void DeleteBuilding(GameObject building)
    {
        _allBuildings.Remove(building);
    }
}
