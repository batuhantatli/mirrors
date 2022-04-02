using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObje : MonoBehaviour
{
    public float mouseRotateSpeed = 15f;
    float rotX;
    float rotY;
     
    void OnMouseDrag()
    {
        rotX = Input.GetAxis("Mouse X") * mouseRotateSpeed*Mathf.Deg2Rad ;
        rotY = Input.GetAxis("Mouse Y") * mouseRotateSpeed * Mathf.Deg2Rad;
        
        transform.RotateAround( - Vector3.up , rotX);

    }
}
