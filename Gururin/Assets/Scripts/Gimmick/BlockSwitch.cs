using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSwitch : MonoBehaviour
{

    public GameObject hideBlock, appearBlock, vCam;
    private bool blocking;
    public float blendSpeed;
    private CriAtomSource _pushSE;
    private Gamecontroller _gameController;

    // Start is called before the first frame update
    void Start()
    {
        blocking = false;
        _pushSE = GameObject.Find("SE_item(CriAtomSource)").GetComponent<CriAtomSource>();
        _gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();
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
        //コントローラーの操作を封じる
        _gameController.isCon = true;
        //ブロックの位置にカメラを移動
        vCam.SetActive(true);

        yield return new WaitForSeconds(blendSpeed);

        //ブロックを消す
       hideBlock.transform.position = new Vector3(1000, 0);
        if(appearBlock != null)
        {
            appearBlock.SetActive(true);
        }

        yield return new WaitForSeconds(blendSpeed);

        //コントローラーの操作を許可
        _gameController.isCon = false;
        //カメラを元に戻す
        vCam.SetActive(false);

        yield break;
    }
}
