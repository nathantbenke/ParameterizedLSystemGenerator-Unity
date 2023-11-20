using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 100.0f;
    [SerializeField] public Slider Zoom;

    void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize += -scrollWheel * zoomSpeed;
        zoomSpeed = Zoom.value;
        
    }
}

