using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSwitch : MonoBehaviour
{

    public GameObject block;
    private bool blocking;

    // Start is called before the first frame update
    void Start()
    {
        blocking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && blocking == false)
        {
            block.SetActive(true);
            blocking = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
