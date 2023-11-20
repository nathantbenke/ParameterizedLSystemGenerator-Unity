using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraDrag : MonoBehaviour
{
    [SerializeField] private float dragSpeed = 0.01f;
    [SerializeField] public Slider drag;

    private Vector3 mouseOrigin;
    [SerializeField] public float dragOffset = 9500; // 6
    // 5 - 1200

    void Update()
    {
        dragSpeed = drag.value;
        HandleMouseInput();
    }

    private void HandleMouseInput()
    {
        //If mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            mouseOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        // Difference between the initial and current mouse positions
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.ScreenToWorldPoint(mouseOrigin);

        // Move camera based on the difference
        //Debug.Log(pos.magnitude);
        if (Camera.main.transform.position.x + (-pos.x * dragSpeed) < dragOffset &&
            Camera.main.transform.position.x + (-pos.x * dragSpeed) > dragOffset * -1 && 
            Camera.main.transform.position.y + (-pos.y * dragSpeed) < dragOffset &&
            Camera.main.transform.position.y + (-pos.y * dragSpeed) > dragOffset * -1) {
            transform.Translate(-pos.x * dragSpeed, -pos.y * dragSpeed, 0);
        }


    }
}

