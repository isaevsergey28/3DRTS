using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Zenject;

public class SpawnButton : MonoBehaviour, IPointerClickHandler
{
    private UnitSpawner _unitSpawner;
    

    private Vector3 _spawnPosition;
    private UnitType _unitType;

    [SerializeField] private Slider _slider;
    [SerializeField] private float _sliderSpeed = 1f;

    private Building _building;
    

    [Inject]
    private void Construct(UnitSpawner unitSpawner)
    {
        _unitSpawner = unitSpawner;
    }
    private void Start()
    {
        if(gameObject.transform.parent.transform.parent.transform.parent.TryGetComponent(out _building))
        {
            _spawnPosition = _building.gameObject.transform.position;
        }
       
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        _building.isRecruitment = true;

        switch(gameObject.name)
        {
            case "MeleeButton":
                _unitType = UnitType.Melee;
                break;
            case "RangedButton":
                _unitType = UnitType.Ranged;
                break;
        }
        StartCoroutine(LoadSlider());
        
    }
    private void Update()
    {
        if (_building.isRecruitment)
        {
            if (_slider.value <= _slider.maxValue)
            {
                _slider.value += _sliderSpeed;
            }
        }
    }
    private IEnumerator LoadSlider()
    {
        yield return new WaitWhile(() => _slider.value < _slider.maxValue);
        _unitSpawner.SpawnUnit(_unitType, new Vector3(_spawnPosition.x + 2f, 0f, _spawnPosition.z));
        _slider.value = 0f;
        _building.isRecruitment = false;
    }
}
