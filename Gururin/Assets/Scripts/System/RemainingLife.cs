using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ぐるりんの残機制御
/// </summary>

public class RemainingLife : MonoBehaviour
{

    public static int life;
    public static int maxLife;

    // Start is called before the first frame update
    void Start()
    {
        //ぐるりんの残機 タイトル画面に戻ってきたら全機回復
        //難易度に応じて適宜変更
        life = 6;
        maxLife = life;
    }
}
