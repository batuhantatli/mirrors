using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjFollowMouse : MonoBehaviour
{
    PlaceObjectOnGrid placeObjectOnGrid;
    public bool isOnGrid;
    void Start()
    {
        placeObjectOnGrid = FindObjectOfType<PlaceObjectOnGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isOnGrid)
        {
        transform.position = placeObjectOnGrid.smoothMousePosition + new Vector3(0,1f,0);
        }
        else
        {
        }
    }

}
