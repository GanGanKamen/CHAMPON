using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassEjectSwitch : MonoBehaviour
{

    public GameObject glassBall, glassPos, vCam;
    private CriAtomSource _pushSE;
    private Gamecontroller _gameController;

    private void Start()
    {
        _pushSE = GameObject.Find("SE_item(CriAtomSource)").GetComponent<CriAtomSource>();
        _gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _pushSE.Play();
            StartCoroutine(VCam());
        }
    }

    IEnumerator VCam()
    {
        //コントローラーの操作を封じる
        _gameController.isCon = true;
        //ガラス玉の位置にカメラを移動
        vCam.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        //ガラス玉をインスタンス
        var glass = Instantiate(glassBall);
        var pos = glassPos.transform.position;
        glass.transform.position = new Vector3(pos.x, pos.y, pos.z);

        yield return new WaitForSeconds(1.5f);

        //コントローラーの操作を許可
        _gameController.isCon = false;
        //カメラを元に戻す
        vCam.SetActive(false);

        yield break;
    }
}
