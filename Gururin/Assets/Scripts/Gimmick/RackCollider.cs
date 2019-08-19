using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RackCollider : MonoBehaviour
{

    public GameObject traFixed;
    private FlagManager flagManager;

    // Start is called before the first frame update
    void Start()
    {
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            traFixed.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            Invoke("Regene", 0.3f);
        }
    }

    void Regene()
    {
        traFixed.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (flagManager.VGcol)
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
