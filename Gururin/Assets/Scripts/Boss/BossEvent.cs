﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class BossEvent : MonoBehaviour
{
    public Gamecontroller gamecontroller;
    public string[] textMessage;

    [SerializeField] private RectTransform topBand, bottomBand;
    [SerializeField] private Vector2 topStart, topOver;
    [SerializeField] private Vector2 bottomStart, bottomOver;

    private Vector2 topDis, bottomDis;
    private bool stop = true;

    [SerializeField] private BossChara bossChara;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject[] wind;
    [SerializeField] private FanWingRotation[] fans;
    [SerializeField] private RectTransform window;
    [SerializeField] private Animator windowAnim;
    [SerializeField] private Text windowText;
    [SerializeField] private GanGanKamen.BossBalloon bossBalloon;

    private PlayerMove player;

    [SerializeField] private Image fader;
    private bool fadeOut = false;
    private float fadeAlpha = 1;

    [SerializeField] private CinemachineVirtualCamera[] virtualCameras;

    public Slider finish;

    public BossCadaver bossCadaver;
    [SerializeField] GameObject nextStage;
    private bool goBack;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        topBand.localPosition = topStart;
        bottomBand.localPosition = bottomStart;
        topDis = topStart;
        bottomDis = bottomStart;
        fadeAlpha = 1;
    }

    void Start()
    {
        StartCoroutine(StartEvent());
    }

    // Update is called once per frame
    void Update()
    {
        if(stop == false)
        {
            topBand.localPosition = Vector2.Lerp(topBand.localPosition, topDis, Time.deltaTime);
            bottomBand.localPosition = Vector2.Lerp(bottomBand.localPosition, bottomDis, Time.deltaTime);
        }
        fader.color = new Color(40f/255f, 40f/255f, 40f/255f, fadeAlpha);
        if (fadeOut)
        {
            fadeAlpha -= Time.deltaTime;
            if(fadeAlpha <= 0)
            {

                fadeOut = false;
            }
        }
        if (goBack)
        {
            player.transform.position += new Vector3(0, Time.deltaTime * 5f, 0);
        }
    }

    private IEnumerator StartEvent()
    {
        gamecontroller.isCon = true;
        MovieCutIn();
        while(Mathf.Abs(topBand.localPosition.y- topDis.y) > 1f)
        {
            yield return null;
        }
        stop = true;
        bossChara.gameObject.SetActive(true);
        yield return new WaitForSeconds(6f);
        Vector2 force = new Vector2(0, 300f);
        player.GetComponent<Rigidbody2D>().AddForce(force);
        while(bossChara.hasDown == false)
        {
            yield return null;
        }
        yield return new WaitForSeconds(3f);
        bossChara.Recovery();
        for(int i = 0; i < fans.Length; i++)
        {
            fans[i].windAct = true;
        }
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < wind.Length; i++)
        {
            wind[i].SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        windowText.text = textMessage[0];
        windowAnim.SetBool("Close", false);
        windowAnim.SetBool("Open", true);
        yield return new WaitForSeconds(1f);
        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }
        
        windowAnim.SetBool("Open", false);
        windowAnim.SetBool("Close", true);
        MovieCutOut();
        while (Mathf.Abs(topBand.localPosition.y - topDis.y) > 1f)
        {
            yield return null;
        }
        bossChara.gameObject.SetActive(false);
        boss.SetActive(true);
        gamecontroller.isCon = false;
        while(bossBalloon.lifes > 0)
        {
            yield return null;
        }
        gamecontroller.isCon = true;
        yield return new WaitForSeconds(3f);
        fader.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        fadeOut = true;
        player.transform.eulerAngles = new Vector3(0, 0, -90f);
        player.transform.position = new Vector3(3.5f, -1f, 0);
        player.balloon.SetActive(false);
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        yield return null;
        while(fadeOut == true)
        {
            yield return null;
        }
        MovieCutIn();
        while (Mathf.Abs(topBand.localPosition.y - topDis.y) > 1f)
        {
            yield return null;
        }
        stop = true;

        player.animator.enabled = true;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        virtualCameras[1].Priority = 11;
        yield return new WaitForSeconds(0.5f);
        player.animator.SetTrigger("Jump");
        yield return new WaitForSeconds(2f);
        player.animator.SetTrigger("Kick");
        virtualCameras[1].Priority = 1;
        yield return new WaitForSeconds(1f);
        virtualCameras[2].Priority = 11;
        player.animator.enabled = false;

        window.localPosition = Vector3.zero;
        windowText.text = textMessage[1];
        windowAnim.SetBool("Open", true);
        windowAnim.SetBool("Close", false);

        yield return new WaitForSeconds(0.5f);
        gamecontroller.isCon = false;
        player.finishMode = true;
        finish.gameObject.SetActive(true);
        while (finish.value < finish.maxValue)
        {
            yield return null;
        }
        nextStage.SetActive(true);
        windowAnim.SetBool("Open", false);
        windowAnim.SetBool("Close", true);

        gamecontroller.isCon = true;
        finish.gameObject.SetActive(false);
        bossCadaver.BreakUp();

        yield return new WaitForSeconds(0.5f);

        goBack = true;
    }

    private void MovieCutIn()
    {
        stop = false;
        topDis = topOver;
        bottomDis = bottomOver;
    }

    private void MovieCutOut()
    {
        stop = false;
        topDis = topStart;
        bottomDis = bottomStart;
    }
}