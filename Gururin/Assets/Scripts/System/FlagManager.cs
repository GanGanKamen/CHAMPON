﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    public bool moveStop; //ぐるりんの動きを止める
    public bool returnGravity; //重力を元に戻す
    public bool[] isMove_VG; //重力変化
    public bool pressParm; //GamecontrollerのisPressを許可
    public bool jumping;
    public bool VGcol;
    public bool activeBalloon;
    public bool standFirm_Face; //踏ん張り顔
    public bool surprise_Face; //驚き顔
    public bool velXFixed;
    public bool stageClear;

    // Start is called before the first frame update
    void Start()
    {
        pressParm = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}