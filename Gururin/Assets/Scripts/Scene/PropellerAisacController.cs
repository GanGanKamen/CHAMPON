﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プロペラステージ1BGMのAISAC制御
/// </summary>


public class PropellerAisacController : MonoBehaviour
{

    private CriAtomSource source;
    private string aisacControllerName_B = "piopellerBGM_B";
    private string aisacControllerName_C = "piopellerBGM_C";
    private string aisacControllerName_D = "piopellerBGM_D";
    //public bool[] _playLimit;
    public float[] currentControlValue;
    [SerializeField] RotationCounter[] rotationCounter;

    private void Awake()
    {
        source = GetComponent<CriAtomSource>();

        //AISACのコントロール値を0.0fにする
        currentControlValue[0] = 0.0f;
        currentControlValue[1] = 0.0f;
        currentControlValue[2] = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        source.SetAisacControl(aisacControllerName_B, currentControlValue[0]);
        source.SetAisacControl(aisacControllerName_C, currentControlValue[1]);
        source.SetAisacControl(aisacControllerName_D, currentControlValue[2]);
        //ステージ開始時にBGMを鳴らす
        //source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (source == null) return;

        //countが加算されたらAISACのコントロール値を上げる
        if (rotationCounter[0].countPlus)
        {
            currentControlValue[0] += 0.01f;
            if (1.0f < currentControlValue[0]) currentControlValue[0] = 1.0f;
            source.SetAisacControl(aisacControllerName_B, currentControlValue[0]);
        }
        //中間地点からスタートしたとき
        else if (RemainingLife.waypoint)
        {
            currentControlValue[0] += 1.0f;
            if (1.0f < currentControlValue[0]) currentControlValue[0] = 1.0f;
            source.SetAisacControl(aisacControllerName_B, currentControlValue[0]);
        }

        //countが加算されたらAISACのコントロール値を上げる
        if (rotationCounter[1].countPlus)
        {
            currentControlValue[1] += 0.01f;
            if (1.0f < currentControlValue[1]) currentControlValue[1] = 1.0f;
            source.SetAisacControl(aisacControllerName_C, currentControlValue[1]);
        }

        //countが加算されたらAISACのコントロール値を上げる
        if (rotationCounter[2].countPlus)
        {
            currentControlValue[2] += 0.01f;
            if (1.0f < currentControlValue[2]) currentControlValue[2] = 1.0f;
            source.SetAisacControl(aisacControllerName_D, currentControlValue[2]);
        }
    }
}
