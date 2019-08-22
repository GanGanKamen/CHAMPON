using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSwitch : MonoBehaviour
{

    public GameObject block, vCam;
    private bool blocking;
    public float blendSpeed;
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
            //block.SetActive(false);
            //block.transform.position = new Vector3(1000, 0);
            StartCoroutine(VCam());
        }
    }

    IEnumerator VCam()
    {
        //ブロックの位置にカメラを移動
        vCam.SetActive(true);

        yield return new WaitForSeconds(blendSpeed);

        //ブロックを消す
       block.transform.position = new Vector3(1000, 0);

        yield return new WaitForSeconds(blendSpeed);

        //カメラを元に戻す
        vCam.SetActive(false);

        yield break;
    }
}
