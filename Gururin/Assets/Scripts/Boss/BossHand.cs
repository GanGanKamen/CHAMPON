using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    public class BossHand : MonoBehaviour
    {
        private PlayerMove player;
        private Gamecontroller gameController;
        private GameObject handParent;


        private Vector3 startPos;

        [SerializeField] private Vector2 moveRangeX;
        [SerializeField] private Vector2 moveRangeY;
        [SerializeField] private GearGimmick thisGear;
        [SerializeField] private BossStageGear floorGimickGear;
        [SerializeField] private Vector3 GearPosOffest;
        [SerializeField] private BossBalloon bossBalloon;
        [Range(-1, 1)] [SerializeField] int direction;
        private Vector2 distinationPos;
        private bool hitPlayer = false;

        public enum Pattern
        {
            RandomWalk,
            Stop,
            Kill,
            Attack,
            GearTurn,
            Recovery
        }
        public Pattern pattern;

        public enum Hand
        {
            Left,
            Right
        }
        public Hand hand;

        private float moveSpeed;
        [SerializeField] private Transform deathZones;
        [SerializeField] private float attackProbability;
        private float attackCount;
        private CapsuleCollider2D capsuleCollider;

        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
            gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();
            capsuleCollider = GetComponent<CapsuleCollider2D>();
            handParent = transform.parent.gameObject;
            pattern = Pattern.RandomWalk;
            startPos = handParent.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            handParent.transform.position = Vector2.Lerp(handParent.transform.position, distinationPos, moveSpeed);
            switch (pattern)
            {
                case Pattern.RandomWalk:
                    RandomWalk();
                    ColliderCancel();
                    break;
                case Pattern.Kill:
                    attackCount = 0;
                    moveSpeed = Time.deltaTime / 2f;
                    if (Vector3.Distance(handParent.transform.position, distinationPos) < 0.5f)
                    {
                        pattern = Pattern.RandomWalk;
                        distinationPos = new Vector2(Random.Range(moveRangeX.x, moveRangeX.y),
                Random.Range(moveRangeY.x, moveRangeY.y));
                    }
                    ColliderCancel();
                    break;
                case Pattern.Attack:
                    attackCount += Time.deltaTime;
                    moveSpeed = Time.deltaTime;
                    distinationPos = player.transform.position;
                    if (//(Vector3.Distance(handParent.transform.position, distinationPos) < 0.5f && hitPlayer == false)
                        attackCount >= 6f
                        || player.nowBossHand != null)
                    {
                        attackCount = 0;
                        pattern = Pattern.RandomWalk;
                        distinationPos = new Vector2(Random.Range(moveRangeX.x, moveRangeX.y),
                Random.Range(moveRangeY.x, moveRangeY.y));
                    }
                    ColliderCancel();
                    break;
                case Pattern.GearTurn:
                    distinationPos = floorGimickGear.transform.position + GearPosOffest;
                    ColliderCancel();
                    if(floorGimickGear.moveFloor.isLimit == true)
                    {
                        pattern = Pattern.RandomWalk;
                        distinationPos = new Vector2(Random.Range(moveRangeX.x, moveRangeX.y),
                Random.Range(moveRangeY.x, moveRangeY.y));
                    }
                    if (Vector3.Distance(handParent.transform.position, distinationPos) < 0.4f)
                    {
                        floorGimickGear.GearTurn(true, false);
                        attackCount += Time.deltaTime;
                        if (attackCount >= 3f)
                        {
                            pattern = Pattern.RandomWalk;
                            distinationPos = new Vector2(Random.Range(moveRangeX.x, moveRangeX.y),
                    Random.Range(moveRangeY.x, moveRangeY.y));
                        }
                    }
                    moveSpeed = Time.deltaTime;
                    break;
                case Pattern.Recovery:
                    distinationPos = startPos;
                    capsuleCollider.enabled = false;
                    if (Vector3.Distance(handParent.transform.position, distinationPos) < 0.2f && player.isJump == true
                        && bossBalloon.status != BossBalloon.Status.Hit)
                    {
                        pattern = Pattern.RandomWalk;
                    }
                    moveSpeed = Time.deltaTime * 2;
                    break;
            }


            if (gameController.isPress && hitPlayer)
            {
                if (gameController.AxB.z < 0)
                {
                    handParent.transform.Rotate(0, 0, thisGear.rotSpeed);
                }
                else if (gameController.AxB.z > 0)
                {
                    handParent.transform.Rotate(0, 0, -thisGear.rotSpeed);
                }

            }
            else if (hitPlayer == false)
            {
                thisGear.gear.transform.Rotate(new Vector3(0.0f, 0.0f, thisGear.rotSpeed * direction));
            }

        }
        private void ColliderCancel()
        {
            if (player.nowBossHand != null && player.nowBossHand != this)
            {
                capsuleCollider.enabled = false;
            }
            else
            {
                capsuleCollider.enabled = true;
            }
        }

        private void RandomWalk()
        {
            moveSpeed = Time.deltaTime / 2f;
            if (Vector3.Distance(handParent.transform.position, distinationPos) < 0.5f)
            {
                distinationPos = new Vector2(Random.Range(moveRangeX.x, moveRangeX.y),
                Random.Range(moveRangeY.x, moveRangeY.y));
            }
            /*
            attackCount += Time.deltaTime;
            if (attackCount >= attackProbability)
            {
                attackCount = 0;
                bool isAttack = bossBalloon.HandCheck(hand);
                if (isAttack == true)
                {
                    pattern = Pattern.Attack;
                }
                
                else
                {
                    pattern = Pattern.GearTurn;
                }
            }*/
        }

        public void AttachPlayer()
        {
            player.transform.parent = this.transform;
            player.nowBossHand = this;
            hitPlayer = true;
            pattern = Pattern.Kill;
            distinationPos = deathZones.position;

        }

        public void Separate()
        {
            player.transform.parent = null;
            hitPlayer = false;
            pattern = Pattern.Recovery;
            distinationPos = startPos;
        }
    }
}


