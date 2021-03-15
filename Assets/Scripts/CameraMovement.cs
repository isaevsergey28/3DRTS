using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private int _cameraWidth;
    private int _cameraHeight;

    [SerializeField] private float _speed;

    private float _moveTriggerForCam = 20f;

    private void Start()
    {
        _cameraWidth = Screen.width;
        _cameraHeight = Screen.height;
    }
    private void Update()
    {
        Vector3 newCamPos = transform.position;
        if(Input.mousePosition.x < _moveTriggerForCam)
        {
            newCamPos.x -= Time.fixedDeltaTime * _speed;
        }
        else if(Input.mousePosition.x > Screen.width - _moveTriggerForCam)
        {
            newCamPos.x += Time.fixedDeltaTime * _speed;
        }
        else if (Input.mousePosition.y < _moveTriggerForCam)
        {
            newCamPos.z -= Time.fixedDeltaTime * _speed;
        }
        else if (Input.mousePosition.y > Screen.height - _moveTriggerForCam)
        {
            newCamPos.z += Time.fixedDeltaTime * _speed;
        }
        transform.position = new Vector3(Mathf.Clamp(newCamPos.x, -40f,40f), newCamPos.y, Mathf.Clamp(newCamPos.z, -40f, 35f));
    }
}
