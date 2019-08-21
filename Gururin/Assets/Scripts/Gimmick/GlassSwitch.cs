using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassSwitch : MonoBehaviour
{

    public GameObject door;
    private CriAtomSource _pushSE;

    private void Start()
    {
        _pushSE = GameObject.Find("SE_item(CriAtomSource)").GetComponent<CriAtomSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject)
        {
            _pushSE.Play();
            door.SetActive(false);
            Destroy(other.gameObject);
            gameObject.SetActive(false);
        }
    }
}
