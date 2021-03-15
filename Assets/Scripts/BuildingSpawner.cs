using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class BuildingSpawner : MonoBehaviour, IPointerClickHandler
{
    private bool _isClicked = false;
    private bool _isEnoughMoney = false;
    [SerializeField] private int _priceForBuilding;

    [SerializeField] private GameObject _gameInfoObject;
    private GameInfo _gameInfoScript;

    [SerializeField] private GameObject _allBuildingsObject;

    private BuildingPanelSystem _allBuilds;
    private GameObject _activeBuilding;

    public BuildingType buildingType;

    private IBuildingFactory _buildingFactory;

    private Renderer _buildingRenderer;

    [Inject]
    private void Construct(IBuildingFactory buildingFactory)
    {
        _buildingFactory = buildingFactory;
        _buildingFactory.Load();
    }

    private void Start()
    {
        _gameInfoScript = _gameInfoObject.GetComponent<GameInfo>();
        _allBuilds = _allBuildingsObject.GetComponent<BuildingPanelSystem>();
    }
    private void Update()
    {
        if(_isClicked)
        {   
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, new Vector3(0f, 0.75f,0f));

            if (groundPlane.Raycast(ray, out float hit))
            {
                _activeBuilding.transform.position = ray.GetPoint(hit);
                if (Input.GetMouseButtonDown(0))
                {
                    _isClicked = false;
                     if (_allBuildingsObject.TryGetComponent(out BuildingPanelSystem buildPanelSys))
                    {
                        buildPanelSys.enabled = true;
                    }
                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    _allBuilds.DeleteBuilding(_activeBuilding);
                    Destroy(_activeBuilding);
                    _isClicked = false;
                    _gameInfoScript.playerCash += _priceForBuilding;
                }

            }

        }
    }
    public void SpawnUnit(BuildingType buildingType, Vector3 at)
    {
        _gameInfoScript.playerCash -= _priceForBuilding;
        _activeBuilding = _buildingFactory.Create(buildingType, at);
        _buildingRenderer = _activeBuilding.GetComponent<Renderer>();
        _allBuilds.AddBuilding(_activeBuilding);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(CheckForMoney())
            {
                SpawnUnit(buildingType, hit.point);
                _isClicked = true;
                if (_allBuildingsObject.TryGetComponent(out BuildingPanelSystem buildPanelSys))
                {
                    buildPanelSys.enabled = false;
                }
            }
           
        }
       
    }
    private bool CheckForMoney()
    {
        return _isEnoughMoney = _gameInfoScript.playerCash - _priceForBuilding > 0 ? true : false;
    }
}
