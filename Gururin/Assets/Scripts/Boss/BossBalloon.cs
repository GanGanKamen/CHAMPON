﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GanGanKamen
{
    public class BossBalloon : MonoBehaviour
    {
        private PlayerMove player;
        public BossHand[] hands; //0:lefthand 1:righthand
        [SerializeField] private int lifes;

        private SpriteRenderer sprite;
        private float recovery = 0;

        [SerializeField] private float attackProbability;
        private float attackCount;

        public enum Status
        {
            StandBy,
            Action,
            Hit
        }
        public Status status;
        // Start is called before the first frame update
        void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        }

        // Update is called once per frame
        void Update()
        {
            if (lifes == 0)
            {
                Debug.Log("Clear");
            }
            DamageHit();

            StatusManager();
        }

        private void StatusManager()
        {
            if (hands[0].pattern == BossHand.Pattern.RandomWalk && hands[1].pattern == BossHand.Pattern.RandomWalk)
            {
                status = Status.StandBy;
            }
            else if(status != Status.Hit)
            {
                status = Status.Action;
                attackCount = 0;
            }
            if (status == Status.StandBy)
            {
                attackCount += Time.deltaTime;
                if (attackCount >= attackProbability)
                {
                    attackCount = 0;
                    for (int i = 0; i < hands.Length; i++)
                    {
                        bool isAttack = HandCheck(hands[i].hand);
                        if (isAttack == true)
                        {
                            hands[i].pattern = BossHand.Pattern.Attack;
                        }
                        else
                        {
                            hands[i].pattern = BossHand.Pattern.GearTurn;
                        }
                    }
                    status = Status.Action;
                }
            }
        }

        private void DamageHit()
        {
            if (status == Status.Hit)
            {
                sprite.color = Color.red;
                recovery += Time.deltaTime;
                for(int i = 0; i < hands.Length; i++)
                {
                    hands[i].pattern = BossHand.Pattern.Stop;
                }
                if (recovery > 2f)
                {
                    recovery = 0;
                    for (int i = 0; i < hands.Length; i++)
                    {
                        hands[i].pattern = BossHand.Pattern.RandomWalk;
                    }
                }
            }
            else
            {
                sprite.color = Color.white;
            }
        }


        public bool HandCheck(BossHand.Hand hand)
        {
            switch (hand)
            {
                case BossHand.Hand.Left:
                    if (player.transform.position.x <= transform.position.x)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case BossHand.Hand.Right:
                    if (player.transform.position.x >= transform.position.x)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                default:
                    return false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (status == Status.Hit) return;
            if (collision.CompareTag("Player"))
            {
                PlayerMove player = collision.gameObject.GetComponent<PlayerMove>();
                if (player.nowBossHand != null)
                {
                    return;
                }
                status = Status.Hit;
                lifes--;

            }
        }
    }
}
