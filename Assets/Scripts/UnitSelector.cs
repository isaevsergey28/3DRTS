using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{
    [SerializeField] private Texture _backgroundSelection;

    private Ray _ray;
    private RaycastHit _hit;
    private bool _isSelecting;
    private Vector3 _mouseStartPosition;
    private Vector3 _selectionStartPosition;
    private Vector3 _selectionEndPosition;

    private float _currentMouseXPosition;
    private float _currentMouseYPosition;

    private float _selectionWidth;
    private float _selectionHeight;
    
    private ActiveUnits _activeUnits;

    private void Start()
    {
        _activeUnits = GetComponent<ActiveUnits>();
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _isSelecting = true;
            _mouseStartPosition = Input.mousePosition;

            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit))
            {
                _selectionStartPosition = _hit.point;
            }
        }
        _currentMouseXPosition = Input.mousePosition.x;
        _currentMouseYPosition = Screen.height - Input.mousePosition.y;

        _selectionWidth = _mouseStartPosition.x - _currentMouseXPosition;
        _selectionHeight = Input.mousePosition.y - _mouseStartPosition.y;

        
        if(Input.GetMouseButtonUp(0))
        {
            _isSelecting = false;

            DeselectUnits();
            if (_mouseStartPosition == Input.mousePosition)
            {
                SingleSelect();
            }
            else
            {
                MultiSelect();
            }
        }
        
    }
    private void OnGUI()
    {
        if(_isSelecting)
        {
            GUI.DrawTexture(new Rect(_currentMouseXPosition, _currentMouseYPosition, _selectionWidth, _selectionHeight), _backgroundSelection);
        }
    }
    private void DeselectUnits()
    {
        foreach (GameObject unit in _activeUnits.GetUnits())
        {
            unit.GetComponent<UnitController>().isSelected = false;
        }
    }

    private void MultiSelect()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(_ray, out _hit))
        {
            _selectionEndPosition = _hit.point;
        }

        foreach(GameObject unit in _activeUnits.GetUnits())
        {
            float x = unit.transform.position.x;
            float z = unit.transform.position.z;

            if((x > _selectionStartPosition.x && x < _selectionEndPosition.x) || (x < _selectionStartPosition.x && x > _selectionEndPosition.x))
            {
                if((z > _selectionStartPosition.z && z < _selectionEndPosition.z) || (z < _selectionStartPosition.z && z > _selectionEndPosition.z))
                {
                    unit.GetComponent<UnitController>().isSelected = true;
                }
            }
        }
    }

    private void SingleSelect()
    {
        if (_hit.collider.gameObject.tag == "Unit")
        {
            _hit.collider.gameObject.transform.TryGetComponent(out UnitController unit);
            unit.isSelected = true;
        }
    }
}
