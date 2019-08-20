using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchGimick : MonoBehaviour
{
    public GameObject pointer1,pointer2;
    public float pointer1AngleZ, pointer2AngleZ;
    [Range(-1, 1)] public int direction;//0静止、1時計回り、-1逆時計 
    // Start is called before the first frame update
    void Start()
    {
        pointer1AngleZ = pointer1.transform.eulerAngles.z;
        pointer2AngleZ = pointer2.transform.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(pointer1AngleZ != pointer1.transform.eulerAngles.z)
        {
            pointer1AngleZ = pointer1.transform.eulerAngles.z;
        }
    }

    public void PointerRotate(bool PlusOrMinus)
    {
        if(PlusOrMinus == true)
        {
            pointer1.transform.Rotate(0, 0, -360f * Time.deltaTime);
            pointer2.transform.Rotate(0, 0, -60f * Time.deltaTime);
            direction = 1;
        }
        else
        {
            pointer1.transform.Rotate(0, 0, 360f * Time.deltaTime);
            pointer2.transform.Rotate(0, 0, 60f * Time.deltaTime);
            direction = -1;
        }
    }
}
