using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class End : MonoBehaviour
{
    private Material currentColor;
    private WaitForSeconds timer;

    public bool isComplated; 
    public bool stopper;
    [SerializeField] private Material newColor;
    [SerializeField] private float transitTime;


    private void Awake() {
        currentColor = gameObject.GetComponent<MeshRenderer>().material;
    }

    private void Start() 
    {
        timer = new WaitForSeconds(transitTime);

        isComplated=false;
        stopper = false;
    }

    public IEnumerator EndPointComplated()
    {
        if(stopper == false)
        {
            stopper = true;
            ColorChange();
            yield return timer;
            UI_Buttons.Instance.NextLevelButtonActivate();
        }
        
    }

    public void ColorChange()
    {
        currentColor.DOColor(newColor.color,transitTime);
        isComplated = true;
    }
    public void ColorChangeBack()
    {
        //currentColor.DOColor ()
    }
}
