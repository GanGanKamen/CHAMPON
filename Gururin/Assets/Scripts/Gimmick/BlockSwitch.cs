using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSwitch : MonoBehaviour
{

    public GameObject block, vCam;
    private bool blocking;
    private CriAtomSource _pushSE;

    // Start is called before the first frame update
    void Start()
    {
        blocking = false;
        _pushSE = GameObject.Find("SE_item(CriAtomSource)").GetComponent<CriAtomSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && blocking == false)
        {
            _pushSE.Play();
            blocking = true;
            StartCoroutine(VCam());
        }
    }

    IEnumerator VCam()
    {
        //ガラス玉の位置にカメラを移動
        vCam.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        //ブロック出現
        block.SetActive(true);

        yield return new WaitForSeconds(2.0f);

        //カメラを元に戻す
        vCam.SetActive(false);

        yield break;
    }
}
