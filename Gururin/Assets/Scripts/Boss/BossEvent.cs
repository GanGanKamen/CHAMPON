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

    [SerializeField] private GameObject bossChara,boss;
    [SerializeField] private GameObject[] wind;
    // Start is called before the first frame update
    private void Awake()
    {
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
        bossChara.SetActive(true);
        yield return new WaitForSeconds(5f);
        for(int i = 0; i < wind.Length; i++)
        {
            wind[i].SetActive(true);
        }
        yield return new WaitForSeconds(1f);
        MovieCutOut();
        while (Mathf.Abs(topBand.localPosition.y - topDis.y) > 1f)
        {
            yield return null;
        }
        bossChara.SetActive(false);
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
