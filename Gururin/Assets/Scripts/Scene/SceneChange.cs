using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移制御
/// 主にステージクリア時
/// </summary>

public class SceneChange : MonoBehaviour
{

    public SceneObject changeScene;
    public bool button;

    private FlagManager flagManager;

    private bool _volumeDown;
    private float _volume;

    public SceneObject[] bossScenes;
    // Start is called before the first frame update
    void Start()
    {
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
        
        
        button = false;
        _volumeDown = false;
        _volume = 1.0f;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            flagManager.velXFixed = true;
            //ぐるりんの動きを止める
            flagManager.moveStop = true;
            //3.5秒遅延を入れる(ゴールSEを入れるため)
            _volumeDown = true;
            Invoke("Scene", 3.5f);
    }
    }

    void Scene ()
    {
        bool isBoss = false;
        foreach (SceneObject scene in bossScenes)
        {
            if (scene.ToString() == changeScene.ToString())
            {
                isBoss = true;
            }
        }
        if (isBoss) RemainingLife.beforeBossLife = RemainingLife.life;
        SceneManager.LoadScene(changeScene);
    }

    // Update is called once per frame
    void Update()
    {
        if (_volumeDown)
        {
            //ゴール時にボリュームをマイナスする(フェードアウト)
            _volume -= 0.01f;
            //BGMカテゴリとSEカテゴリを指定
            CriAtom.SetCategoryVolume("BGM", _volume);
            CriAtom.SetCategoryVolume("SE", _volume);

            if (_volume == 0.0f)
            {
                _volume = 0.0f;
            }
        }
        if (button)
        {
            Invoke("GameStart", 1.5f);
        }
    }

    void GameStart()
    {
        bool isBoss = false;
        foreach(SceneObject scene in bossScenes)
        {
            if(scene.ToString() == changeScene.ToString())
            {
                isBoss = true;
            }
        }
        if (isBoss) RemainingLife.beforeBossLife = RemainingLife.life;
        SceneManager.LoadScene(changeScene);
        button = false;
    }
}
