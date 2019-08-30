using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    //public Data data;
    //public GameObject configCanvas;

    public SceneChange sceneChange;

    //効果音
    private CriAtomSource _startSE;
    private bool _fadeOut;

    private int _cnt;
    private float _volume;

    // Start is called before the first frame update
    void Start()
    {
        //if (GameObject.Find("Data") != null) data = GameObject.Find("Data").GetComponent<Data>();
        //configCanvas = GameObject.Find("ConfigCanvas");
        _startSE = GetComponent<CriAtomSource>();

        _volume = 1.0f;
        _fadeOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_fadeOut)
        {
            _volume -= 0.01f;
            //BGMのボリュームを段階的に下げる
            CriAtom.SetCategoryVolume("BGM", _volume);

            if (_volume == 0.0f) _volume = 0.0f;
        }
    }
    public void OnClick()
    {

        _cnt++;
        //一度だけStartSEを鳴らす
        if (_cnt == 1)
        {
            _startSE.Play();
        }
        _fadeOut = true;
        //Debug.Log("CLICK");
        sceneChange.button = true;
        //data.destroy = true;
        Debug.Log("x");

        /*if(SceneManager.GetActiveScene().name == "Title")
        {

        }
        else
        {
            //Destroy(configCanvas);
        }*/
    }
}
