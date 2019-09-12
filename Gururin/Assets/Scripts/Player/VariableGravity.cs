using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ラックと接触したときにくっつく動き
/// 重力変化
/// </summary>

public class VariableGravity : MonoBehaviour
{

    private Rigidbody2D _rb2d;

    private PlayerMove playerMove;
    private CriAtomSource _jumpSE;
    private Gamecontroller gameController;
    private FlagManager flagManager;

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMove>();
        _jumpSE = GetComponent<CriAtomSource>();
        gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //上方向に張り付き
        if (other.CompareTag("Wall_U"))
        {
            //重力変化
            Physics2D.gravity = new Vector2(0.0f, 9.81f);
            flagManager.isStick = true;

            //速度再設定
            playerMove.speed[0] = 5.0f;
            playerMove.speed[1] = 0.0f;
            //PlayerMoveによる移動を止める
            playerMove.isMove = false;
            playerMove.isJump = false;

            flagManager.returnGravity = false;
            //上向きの重力がある状態
            flagManager.isMove_VG[0] = true;
        }

        //右方向に張り付き
        if (other.CompareTag("Wall_R"))
        {
            Physics2D.gravity = new Vector2(9.81f, 0.0f);
            flagManager.isStick = true;

            //速度の更新を止める
            playerMove.setSpeed = false;
            playerMove.speed[0] = 0.0f;
            playerMove.speed[1] = 5.0f;
            playerMove.isMove = false;
            playerMove.isJump = false;

            flagManager.returnGravity = false;
            //右向きの重力がある状態
            flagManager.isMove_VG[1] = true;
        }

        //左方向に張り付き
        if (other.CompareTag("Wall_L"))
        {
            Physics2D.gravity = new Vector2(-9.81f, 0.0f);
            flagManager.isStick = true;

            playerMove.setSpeed = false;
            playerMove.speed[0] = 0.0f;
            playerMove.speed[1] = 5.0f;
            playerMove.isMove = false;
            playerMove.isJump = false;

            flagManager.returnGravity = false;
            //左向きの重力がある状態
            flagManager.isMove_VG[2] = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Wall_U") || other.CompareTag("Wall_R") || other.CompareTag("Wall_L"))
        {
            //重力を元に戻す
            flagManager.returnGravity = true;
            flagManager.isMove_VG[0] = false;
            flagManager.isMove_VG[1] = false;
            flagManager.isMove_VG[2] = false;

            //速度の更新
            playerMove.setSpeed = true;
            playerMove.isMove = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //重力を戻す
        if (flagManager.returnGravity)
        {
            Physics2D.gravity = new Vector2(0.0f, -9.81f);
            flagManager.isStick = false;
        }

        if(gameController.AxB.z < 0 && gameController.isPress)
        {
            //右に張り付いたとき右回転で上へ移動
            if(flagManager.isMove_VG[1])
            {
                playerMove.isRot[0] = true;
                flagManager.moveStop = false;
            }

            //上の張り付いたとき右回転で左へ移動、左に張り付いたとき右回転で下へ移動
            else if (flagManager.isMove_VG[0] || flagManager.isMove_VG[2])
            {
                playerMove.isRot[1] = true;
                flagManager.moveStop = false;
            }
        }

        if(gameController.AxB.z > 0 && gameController.isPress)
        {
            //右に張り付いたとき左回転で下へ移動
            if (flagManager.isMove_VG[1])
            {
                playerMove.isRot[1] = true;
                flagManager.moveStop = false;
            }

            //上の張り付いたとき左回転で右へ移動、左に張り付いたとき左回転で上へ移動
            else if (flagManager.isMove_VG[0] || flagManager.isMove_VG[2])
            {
                playerMove.isRot[0] = true;
                flagManager.moveStop = false;
            }
        }
    }

    void FixedUpdate()
    {
        //ジャンプしたとき
        if(flagManager.jumping || gameController.isFlick)
        {
            flagManager.moveStop = false;

            if (playerMove.isMove == false)
            {
                //右に張り付いているとき左方向へジャンプ
                if (flagManager.isMove_VG[1] && gameController.flick_left)
                {
                    Vector2 force = new Vector2(playerMove.jumpSpeed * -1.0f, playerMove.speed[1]);
                    _rb2d.AddForce(force);
                    _jumpSE.Play();
                    gameController.isFlick = false;
                }

                //左に張り付いているとき右方向へジャンプ
                else if (flagManager.isMove_VG[2] && gameController.flick_right)
                {
                    Vector2 force = new Vector2(playerMove.jumpSpeed, playerMove.speed[1]);
                    _rb2d.AddForce(force);
                    _jumpSE.Play();
                    gameController.isFlick = false;
                }

                Physics2D.gravity = new Vector2(0.0f, -9.81f);
                playerMove.isMove = true;
            }

            if (flagManager.isMove_VG[0] && gameController.flick_down)
            {
                _rb2d.AddForce(Vector2.down * playerMove. jumpSpeed);
                _jumpSE.Play();
                gameController.isFlick = false;
            }

            flagManager.jumping = false;
        }
    }
}
