using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResistConveyor : MonoBehaviour
{

    private SurfaceEffector2D _surfaceEffector2D;
    private Gamecontroller _gameController;
    private FlagManager _flagManager;

    public bool resistDirection;
    public float defaultSpeed, resistSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _surfaceEffector2D = GetComponent<SurfaceEffector2D>();
        _gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();
        _flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (resistDirection)
            {
                //左方向に抵抗
                case true:
                    if (_gameController.AxB.z > 0)
                    {
                        _flagManager.standFirm_Face = true;
                        _surfaceEffector2D.speed = resistSpeed;
                    }
                    break;

                //右方向に抵抗
                case false:
                    if (_gameController.AxB.z < 0)
                    {
                        _flagManager.standFirm_Face = true;
                        _surfaceEffector2D.speed = -resistSpeed;
                    }
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //コントローラーが非表示の時ベルトコンベアの速度をDefaultSpeedにする
        if (_gameController.controllerObject.activeInHierarchy == false)
        {
            _surfaceEffector2D.speed = defaultSpeed;

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
