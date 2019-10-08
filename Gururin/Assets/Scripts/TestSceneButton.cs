using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneButton : MonoBehaviour
{
    [SerializeField] private GameObject[] targetObjs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        for(int i = 0; i < targetObjs.Length; i++)
        {
            Destroy(targetObjs[i]);
        }
    }
}
