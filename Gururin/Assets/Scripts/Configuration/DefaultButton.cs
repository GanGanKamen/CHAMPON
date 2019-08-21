using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultButton : MonoBehaviour
{
    public Sensitivity sensitivity;
    public FlickDistance flickDistance;
    public ControllerPosition controllerPosition;


    // Start is called before the first frame update
    void Start()
    {
        if(sensitivity == null)
        {
            sensitivity = GameObject.Find("Sensitivity").GetComponent<Sensitivity>();
        }
        if(flickDistance == null)
        {
            flickDistance = GameObject.Find("FlickDistance").GetComponent<FlickDistance>();
        }
        if(controllerPosition == null)
        {
            controllerPosition = GameObject.Find("ControllerPosition").GetComponent<ControllerPosition>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick()
    {
        sensitivity.OnClick();
        flickDistance.OnClick();
        controllerPosition.OnClick();
    }
}
