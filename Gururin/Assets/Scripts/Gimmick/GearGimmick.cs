using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GearGimmick : MonoBehaviour {

    public GameObject gear; //カウンターとの接触判定を取る歯車
    public GameObject gearPos;  //ぐるりんの位置
    public bool playerHit;  //ぐるりんとの接触判定
    public bool[] moveGear; //歯車とぐるりんの回転方向

    public bool rotParm;
    public float rotSpeed; //歯車とぐるりんの回転速度
    private static CriAtomSource source; //効果音

    private Rigidbody2D _gururinRb2d; //ぐるりんのRigidbody
    private Quaternion _gpQuaternion; //GururinPosの角度

    private PlayerMove playerMove;
    private Gamecontroller gameController;
    private FlagManager flagManager;

    private GanGanKamen.BossHand bossHand;
    private GanGanKamen.BossStageGear stageGear;
    private WatchGear watchGear;

    [SerializeField] BossEvent bossEvent;
    // Start is called before the first frame update
    void Start() {
        source = GetComponent<CriAtomSource>();
        _gururinRb2d = GameObject.Find("Gururin").GetComponent<Rigidbody2D>();
        playerMove = GameObject.Find("Gururin").GetComponent<PlayerMove>();
        gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();
        flagManager = GameObject.Find("FlagManager").GetComponent<FlagManager>();

        _gpQuaternion = gearPos.transform.rotation;

        for(int i = 0; i < moveGear.Length; i++)
        {
            moveGear[i] = true;
        }

        if (GetComponent<GanGanKamen.BossHand>() != null) bossHand = GetComponent<GanGanKamen.BossHand>();
        if (GetComponent<GanGanKamen.BossStageGear>() != null) stageGear = GetComponent<GanGanKamen.BossStageGear>();
        if (GetComponent<WatchGear>() != null) watchGear = GetComponent<WatchGear>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMove>().nowBossHand != null) return;
            if (gameController.isPress)
            {
                gameController.isPress = false;
            }
            ///接触時に即動かせるようにしてしまうとvelocity = Vector2.zero;する前に動かせてしまってずれるので
            ///接触してから一定時間(0.2f～0.3fぐらい？)コントローラーを動かなくするコルーチンを実装する(予定)

            //ぐるりんとGearの接触を感知
            playerHit = true;
            playerMove.gearGimmickHit = true;

            //Playerの回転を許可
            rotParm = true;

            //速度の上書きを止める
            playerMove.setSpeed = false;
            //回転速度を固定
            playerMove.speed[0] = rotSpeed;
            //ぐるりんの移動を止める
            playerMove.isMove = false;
            _gururinRb2d.velocity = Vector2.zero;
            //効果音
            source.Play();

            if (bossHand != null) bossHand.AttachPlayer();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player")&& rotParm)
        {
            _gururinRb2d.isKinematic = true;
            //ぐるりんの位置を固定
            //_gururinRb2d.position = gearPos.transform.position;
           if(GetComponent<GanGanKamen.BossHand>()==null) _gururinRb2d.MovePosition(gearPos.transform.position);
            //ぐるりんの角度を固定
            _gururinRb2d.rotation = _gpQuaternion.eulerAngles.z;

            flagManager.moveStop = true;

            _gururinRb2d.velocity = Vector2.zero;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _gururinRb2d.isKinematic = false;
            playerHit = false;
            playerMove.gearGimmickHit = false;
            playerMove.isMove = true;
            playerMove.setSpeed= true;
            flagManager.moveStop = false;

            other.GetComponent<PlayerMove>().nowBossHand = null;
            if (bossHand != null) bossHand.Separate();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (gameController.AxB.z < 0 && gameController.isPress && playerHit && moveGear[0])
        {
            rotParm = false;
            //ぐるりんの表情を変更
            flagManager.standFirm_Face = true;
            gear.transform.Rotate(new Vector3(0.0f, 0.0f, rotSpeed));
            _gururinRb2d.rotation += -rotSpeed;
            if(stageGear != null)//ボスステージの歯車を回す
            {
                if(stageGear.direction == 1)
                {
                    stageGear.GearTurn(true, true);
                }
                else
                {
                    stageGear.GearTurn(false, true);
                }
            }

            if(watchGear != null)//時計歯車を回す
            {
                watchGear.GearTurn(true);
            }

            if (playerMove.finishMode)//マスターギアを取る
            {
                bossEvent.finish.value += Time.deltaTime;
            }
        }
        else if (gameController.AxB.z > 0 && gameController.isPress && playerHit && moveGear[1])
        {
            rotParm = false;
            flagManager.standFirm_Face = true;
            gear.transform.Rotate(new Vector3(0.0f, 0.0f, -rotSpeed));
            _gururinRb2d.rotation += rotSpeed;
            if (stageGear != null)  //ボスステージの歯車を回す
            {
                if (stageGear.direction == -1)
                {
                    stageGear.GearTurn(true, true);
                }
                else
                {
                    stageGear.GearTurn(false, true);
                }
            }
            if (watchGear != null) //時計歯車を回す
            {
                watchGear.GearTurn(false);
            }
            if (playerMove.finishMode) //マスターギアを取る
            {
                bossEvent.finish.value += Time.deltaTime;
            }
        }
        else
        {
            flagManager.standFirm_Face = false;
        }
    }

    //isPressが押された後(Update後)に判定
    private void LateUpdate()
    {
        if (SceneManager.GetActiveScene().name != "BossScene")
        {
            if (playerMove.isPress && playerHit)
            {
                _gururinRb2d.isKinematic = false;
                //歯車のColliderを消す
                StartCoroutine(Col());
            }
        }
        else
        {
            if (gameController.isFlick && playerHit)
            {
                _gururinRb2d.isKinematic = false;
                //歯車のColliderを消す
                StartCoroutine(Col());
            }
        }

        
    }

    IEnumerator Col()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;

        yield return new WaitForSeconds(0.3f);

        GetComponent<CapsuleCollider2D>().enabled = true;

        yield break;
    }
}
