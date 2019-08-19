using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プロペラステージBGMのAISAC制御
/// </summary>

public class PropellerAisacController : MonoBehaviour
{

    private CriAtomSource source;
    private string aisacControllerName = "Plopeller_wind";
    public float currentControlValue = 0.0f;
    [SerializeField] RotationCounter rotationCounter;

    private void Awake()
    {
        //AISACのコントロール値を0.0fにする
        currentControlValue = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<CriAtomSource>();
        source.SetAisacControl(aisacControllerName, currentControlValue);
        //ステージ開始時にBGMを鳴らす
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //countが加算されたらAISACのコントロール値を上げる
        if (rotationCounter.countPlus)
        {
            currentControlValue += 0.01f;
            if (1.0f < currentControlValue) currentControlValue = 1.0f;
            source.SetAisacControl(aisacControllerName, currentControlValue);
        }
    }
}
