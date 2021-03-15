using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ActivityIndicator : MonoBehaviour
{
    private Camera _mainCamera;
    private UnitController _unitController;
    [SerializeField] private GameObject _indicator;
    [Inject]
    private void Construct(Camera mainCamera)
    {
        _mainCamera = mainCamera;
       
    }
    private void Awake()
    {
        GetComponent<Canvas>().worldCamera = _mainCamera;
    }
    private void Start()
    {
        _unitController = transform.parent.GetComponent<UnitController>();
    }
    private void Update()
    {
        if (_unitController.isSelected)
        {
            _indicator.SetActive(true);
        }
        else
        {
            _indicator.SetActive(false);
        }
    }
}
