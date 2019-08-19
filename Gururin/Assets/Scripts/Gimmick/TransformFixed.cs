using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ラック張り付き時にぐるりんの位置を固定
/// </summary>

public class TransformFixed : MonoBehaviour
{

    private CriAtomSource _gearMesh;
    public GameObject gearPos;

    private FlagManager flagManager;

    // Start is called before the first frame update
    void Start()
    {
        _gearMesh = GetComponent<CriAtomSource>();
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //効果音
            _gearMesh.Play();
            //ぐるりんの位置を固定
            other.transform.position = gearPos.transform.position;
            //ぐるりんの角度を固定
            other.transform.rotation = gearPos.transform.rotation;
            //ぐるりんの動きを止める
            flagManager.moveStop = true;
            flagManager.VGcol = true;
        }
    }
}
