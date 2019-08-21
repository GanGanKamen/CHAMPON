using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEvent : MonoBehaviour
{
    public Gamecontroller gamecontroller;

    [SerializeField] private RectTransform topBand, bottomBand;
    [SerializeField] private Vector2 topStart, topOver;
    [SerializeField] private Vector2 bottomStart, bottomOver;

    private Vector2 topDis, bottomDis;
    private bool stop = true;

    [SerializeField] private BossChara bossChara;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject[] wind;
    [SerializeField] private FanWingRotation[] fans;
    [SerializeField] private Animator windowAnim;

    private PlayerMove player;
    // Start is called before the first frame update
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        topBand.localPosition = topStart;
        bottomBand.localPosition = bottomStart;
        topDis = topStart;
        bottomDis = bottomStart;
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
