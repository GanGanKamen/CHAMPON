using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sensitivity : MonoBehaviour
{
    private Configuration config;
    Slider senSlider;

    // Start is called before the first frame update
    void Start()
    {
        config = GameObject.Find("ConfigCanvas").GetComponent<Configuration>();
        senSlider = GetComponent<Slider>();
        
        //スライダーの最大値の設定
        senSlider.maxValue = 1.5f;

        //スライダーの最小値の設定
        senSlider.minValue = 0.5f;

        //スライダーの現在値の設定
        senSlider.value = 1.0f;

        config.sensitivity = senSlider.value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Method()
    {
        config.sensitivity = senSlider.value;

        Debug.Log("Sensitivity：" + senSlider.value);
        
    }
}
