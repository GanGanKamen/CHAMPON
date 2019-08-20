using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchGear : MonoBehaviour
{
    [SerializeField] private GameObject gear;
    public float rotSpeed;
    public WatchGimick watch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GearTurn(bool direction)
    {
        if (direction == true) watch.PointerRotate(true);
        else
        {
            watch.PointerRotate(false);
        }
    }
}
