using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ぐるりんの動き全般
/// </summary>

public class PlayerMove : MonoBehaviour
{

    public float[] speed; //移動速度
    public bool setSpeed; //基本速度
    public float jumpSpeed; //ジャンプ速度(高さ)
    public bool isMove; //移動許可
    public bool[] isRot; //移動(回転)方向
    public bool isPress; //ジャンプ入力
    public bool isJump; //ジャンプ許可

    private Rigidbody2D _rb2d;
    private CriAtomSource _jumpSE; //ジャンプの効果音

    public bool gearGimmickHit; //ギミックとの接触判定

    private Gamecontroller gameController;
    private FlagManager flagManager;

    public GanGanKamen.BossHand nowBossHand = null; //現在接触しているギミック
    public Animator animator;　//ぐるりんのアニメーター　ボスイベント用
    public GameObject balloon;
    public bool finishMode = false; //ボスにとどめを刺す
    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GameObject.Find("Gururin").GetComponent<Rigidbody2D>();
        _jumpSE = GetComponent<CriAtomSource>();
        gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();

        setSpeed = true;
        isMove = true;
        isJump = false;

        if(animator!=null) animator.enabled = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Jump") && isMove)
        {
            //Jumpタグと接触時にジャンプを可能にする
            isJump = true;

            if (gearGimmickHit == false)
            {
                setSpeed = true;
            }
        }
        /*
        if (other.CompareTag("Right_UI"))
        {
            gameController.isArrowR = true;
        }

        if (other.CompareTag("Left_UI"))
        {
            gameController.isArrowL = true;
        }
        */
    }



    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Jump") && isJump)
        {
            isJump = false;
            gameController.isFlick = false;
        }

        /*
        if (other.CompareTag("Right_UI"))
        {
            gameController.isArrowR = false;
        }

        if (other.CompareTag("Left_UI"))
        {
            gameController.isArrowL = false;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        //速度を毎回上書き
        if (setSpeed)
        {
            speed[0] = gameController.angle * gameController.sensitivity / 5.0f;
            speed[1] = 0.0f;
        }

        //GearGimmickと接触していないとき
        if (gearGimmickHit == false)
        {
            //右へ移動
            if (gameController.AxB.z < 0 && gameController.isPress && isMove)
            {
                isRot[0] = true;
            }

            //左へ移動
            else if (gameController.AxB.z > 0 && gameController.isPress && isMove)
            {
                isRot[1] = true;
            }
        }
    }

    void FixedUpdate()
    {
        //ぐるりんの動きを止める
        if (flagManager.moveStop)
        {
            if (flagManager.velXFixed)
            {
                //横方向の速度のみ0にする
                _rb2d.velocity = new Vector2(0.0f, _rb2d.velocity.y);
            }
            else
            {
                //速度を0にする
                _rb2d.velocity = Vector2.zero;
            }

            //加速度を0にする
            gameController.angle = 0.0f;

            //角度を固定する
            _rb2d.angularVelocity = 0.0f;
        }
        else
        {
            if (isRot[0]&&!finishMode)
            {
                Vector2 force = new Vector2(speed[0], speed[1]);
                _rb2d.AddForce(force);
                isRot[0] = false;
            }
            else if (isRot[1]&&!finishMode)
            {
                //左右のラックに張り付いていないとき
                if (flagManager.isMove_VG[1] == false && flagManager.isMove_VG[2] == false)
                {
                    Vector2 force = new Vector2(-speed[0], speed[1]);
                    _rb2d.AddForce(force);
                }
                //左右のラックに張り付いているとき
                else if (flagManager.isMove_VG[1] || flagManager.isMove_VG[2])
                {
                    Vector2 force = new Vector2(speed[0], -speed[1]);
                    _rb2d.AddForce(force);
                }

                isRot[1] = false;
            }

            if (isPress && !finishMode)
            {
                if (nowBossHand != null)
                {
                    if (gearGimmickHit == false && isJump)
                    {
                        if (gameController.flick_right == false && gameController.flick_left == false)
                        {
                            _rb2d.AddForce(Vector2.up * jumpSpeed);
                            _jumpSE.Play();
                            isJump = false;
                            gameController.isFlick = false;
                        }
                        else if (gameController.flick_right)
                        {
                            Vector2 jumpforce = new Vector2(0.3f / gameController.sensitivity, 1.0f);
                            _rb2d.AddForce(jumpforce * jumpSpeed);
                            _jumpSE.Play();
                            isJump = false;
                            gameController.isFlick = false;
                        }
                        else if (gameController.flick_left)
                        {
                            Vector2 jumpforce = new Vector2(-0.3f / gameController.sensitivity, 1.0f);
                            _rb2d.AddForce(jumpforce * jumpSpeed);
                            _jumpSE.Play();
                            isJump = false;
                            gameController.isFlick = false;
                        }
                    }
                    else if (gearGimmickHit)
                    {
                        //Gearと噛み合っているときにジャンプのみすると後方へジャンプ
                        //移動操作を行いながらジャンプすると移動分の距離が加算される = 後方ジャンプになる？
                        //左方向のみが後方ならばこのままでよいが、右方向へバックジャンプする場合は要改良
                        Vector2 force = new Vector2(-200.0f, 0.0f);
                        _rb2d.AddForce(force);
                        isMove = true;
                    }
                }
                else
                {
                    if (gearGimmickHit == false && isJump)
                    {
                        if (gameController.flick_right == false && gameController.flick_left == false)
                        {
                            _rb2d.AddForce(Vector2.up * jumpSpeed);
                            _jumpSE.Play();
                            isJump = false;
                            gameController.isFlick = false;
                        }
                        else if (gameController.flick_right)
                        {
                            Vector2 jumpforce = new Vector2(0.3f / gameController.sensitivity, 1.0f);
                            _rb2d.AddForce(jumpforce * jumpSpeed);
                            _jumpSE.Play();
                            isJump = false;
                            gameController.isFlick = false;
                        }
                        else if (gameController.flick_left)
                        {
                            Vector2 jumpforce = new Vector2(-0.3f / gameController.sensitivity, 1.0f);
                            _rb2d.AddForce(jumpforce * jumpSpeed);
                            _jumpSE.Play();
                            isJump = false;
                            gameController.isFlick = false;
                        }
                    }
                    else if (gearGimmickHit == false && isJump == false)
                    {
                        if (gameController.flick_up)
                        {
                            _rb2d.AddForce(Vector2.up * jumpSpeed);
                            _jumpSE.Play();
                            isJump = false;
                            gameController.isFlick = false;
                        }
                        if (gameController.flick_down)
                        {
                            _rb2d.AddForce(Vector2.down * jumpSpeed);
                            _jumpSE.Play();
                            isJump = false;
                            gameController.isFlick = false;
                        }
                        if (gameController.flick_right)
                        {
                            _rb2d.AddForce(Vector2.right * jumpSpeed);
                            _jumpSE.Play();
                            isJump = false;
                            gameController.isFlick = false;
                        }
                        if (gameController.flick_left)
                        {
                            _rb2d.AddForce(Vector2.left * jumpSpeed);
                            _jumpSE.Play();
                            isJump = false;
                            gameController.isFlick = false;
                        }
                    }

                }


                isPress = false;
            }
        }
    }
}
