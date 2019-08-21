using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Configuration : MonoBehaviour
{ 
    public GameObject configbuttonOpen;
    public GameObject configbuttonClose;
    public GameObject configwindow;

    public float sensitivity, flickdistance;
    public int controllerposition;
    public bool configbutton = false;

    private CriAtomSource _open, _close;

    [SerializeField] FlagManager flagManager;

    // Start is called before the first frame update
    void Start()
    {
        
        configbuttonClose = GameObject.Find("ConfigButton_Close");
        configbuttonOpen = GameObject.Find("ConfigButton_Open");
        configwindow = GameObject.Find("ConfigWindow");

        //SE追加
        _open = GameObject.Find("SE_WindowOpen(CriAtomSource)").GetComponent<CriAtomSource>();
        _close = GameObject.Find("SE_WindowClose(CriAtomSource)").GetComponent<CriAtomSource>();

        // イベントにイベントハンドラーを追加
        SceneManager.sceneLoaded += SceneLoaded;

        DontDestroyOnLoad(this.gameObject);

        configbuttonClose.SetActive(true);
        configbuttonOpen.SetActive(false);
        configwindow.SetActive(false);

        sensitivity = 1.5f;
        flickdistance = 0.1f;
        controllerposition = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (configbuttonClose == null)
        {
            configbuttonClose = GameObject.Find("ConfigButton_Close");
        }
    }

    public void Method()
    {
        if (configbuttonClose.activeSelf)
        {
            _open.Play();
            configbuttonClose.SetActive(false);
            configbuttonOpen.SetActive(true);
            configwindow.SetActive(true);
            configbutton = true;
        }
        else
        {
            _close.Play();
            configbuttonClose.SetActive(true);
            configbuttonOpen.SetActive(false);
            configwindow.SetActive(false);
            configbutton = false;
        }
    }

    //第一引数(遷移後のシーン),第二引数(シーンの読み込みモード(Single or Additive))
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {

        if (nextScene.name == "Title")
        {
            //Destroy(this.gameObject);
        }
        else if(nextScene.name == "Result")
        {

        }
        else
        {
            flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
        }
    }
}
