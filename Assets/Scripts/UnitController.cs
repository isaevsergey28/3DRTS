using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UnitController : MonoBehaviour
{
    private Vector3 _clickPosition;
    private bool _isClicked = false;
    public bool isSelected { get; set; } = false;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (isSelected)
        {
            if (Input.GetMouseButtonDown(1))
            {
                _isClicked = true;
                Ray ray;
                RaycastHit hit;

                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    _clickPosition = hit.point;
                }
            }
            if (!Mathf.Approximately(transform.position.magnitude, _clickPosition.magnitude) && _isClicked)
            {
                transform.LookAt(_clickPosition);
                transform.position = Vector3.MoveTowards(transform.position, _clickPosition, Time.fixedDeltaTime);
                _animator.SetBool("isWalking", true);
            }
            else
            {
                _isClicked = false;
                _animator.SetBool("isWalking", false);
            }
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }
    }
}
