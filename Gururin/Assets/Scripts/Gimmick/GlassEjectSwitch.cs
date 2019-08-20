using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassEjectSwitch : MonoBehaviour
{

    public GameObject glassBall, glassPos;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var glass = Instantiate(glassBall);
            var pos = glassPos.transform.position;
            glass.transform.position = new Vector3(pos.x, pos.y, pos.z);
        }
    }
}
