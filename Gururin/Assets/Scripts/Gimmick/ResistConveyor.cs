﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ベルトコンベアについて
/// </summary>

public class ResistConveyor : MonoBehaviour
{

    private SurfaceEffector2D _surfaceEffector2D;
    private Gamecontroller _gameController;
    private FlagManager _flagManager;

    public bool[] resistDirection; //抵抗方向、0が左方向、1が右方向
    [SerializeField] private float defaultSpeed; //ベルトコンベアの速度
    private float resistSpeed; //抵抗速度

    // Start is called before the first frame update
    void Start()
    {
        _surfaceEffector2D = transform.GetChild(0).GetComponent<SurfaceEffector2D>();
        _gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();
        _flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();

        //resistSpeedはdefaultSpeedの半分
        resistSpeed = -defaultSpeed / 2.0f;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        //ベルトコンベアに接触時かつコントローラーがアクティブ時
        if (other.CompareTag("Player") && _gameController.controllerObject.activeInHierarchy)
        {
            //左方向に抵抗
            if (resistDirection[0])
            {
                //抵抗
                if (_gameController.AxB.z > 0)
                {
                    _flagManager.standFirm_Face = true;
                    _surfaceEffector2D.speed = resistSpeed;
                }

                //右に移動
                else if (_gameController.AxB.z < 0)
                {
                    _flagManager.standFirm_Face = false;
                    _surfaceEffector2D.speed = defaultSpeed;
                }
            }

            //右方向に抵抗
            if (resistDirection[1])
            {
                //抵抗
                if (_gameController.AxB.z < 0)
                {
                    _flagManager.standFirm_Face = true;
                    _surfaceEffector2D.speed = resistSpeed;
                }

                //左に移動
                else if (_gameController.AxB.z > 0)
                {
                    _flagManager.standFirm_Face = false;
                    _surfaceEffector2D.speed = defaultSpeed;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _flagManager.standFirm_Face)
        {
            _flagManager.standFirm_Face = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //コントローラーが非アクティブ時にベルトコンベアの速度をDefaultSpeedにする
        if (_gameController.controllerObject.activeInHierarchy == false)
        {
            _surfaceEffector2D.speed = defaultSpeed;

            //踏ん張り顔(抵抗)時
            if (_flagManager.standFirm_Face)
            {
                StartCoroutine(FaceChange());
            }
        }
    }

    //表情変化
    IEnumerator FaceChange()
    {
        _flagManager.standFirm_Face = false;

        yield return null;

        _flagManager.surprise_Face = true;

        yield return new WaitForSeconds(0.5f);

        _flagManager.surprise_Face = false;

        yield break;
    }
}
