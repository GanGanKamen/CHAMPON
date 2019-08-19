using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 下からすり抜けられる床
/// </summary>

public class PassFloor : MonoBehaviour
{

    public GameObject[] passFloor;
    private bool setPass = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            setPass = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            setPass = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (setPass)
        {
            passFloor[0].GetComponent<BoxCollider2D>().enabled = false;
            passFloor[1].SetActive(false);
        }
        else if (setPass == false)
        {
            passFloor[0].GetComponent<BoxCollider2D>().enabled = true;
            passFloor[1].SetActive(true);
        }
    }
}
