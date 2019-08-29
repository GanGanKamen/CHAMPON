using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationController : MonoBehaviour
{
    public LanguageText[] sentences; // 文章を格納する
    [SerializeField] Text Text;   // uiTextへの参照

    public GameObject Doctor;
    public GameObject TextUI;
    public GameObject WhiteBack;

    public bool IsConversation, IsDescription;
    public bool sendtext = false;
    public bool feedin = false, feedout = false;
    public int currentSentenceNum = 0; //現在表示している文章番号
    private int preSentenceNum = -1;
    private int displaycount = 0;

    public Vector2 mousePosition;

    public bool[] textFeed;

    public Configuration config;
    public Gamecontroller gameController;

    [SerializeField] private float textWaitTime = 0.1f;
    private IEnumerator nowNobel;
    void Start()
    {
        config = GameObject.Find("ConfigCanvas").GetComponent<Configuration>();
        gameController = GameObject.Find("GameController").GetComponent<Gamecontroller>();

        IsConversation = false;
        IsDescription = false;
        sendtext = false;
        feedin = false;
        feedout = false;
        Doctor.SetActive(false);
        TextUI.SetActive(false);
        WhiteBack.SetActive(false);

        nowNobel = NovelText();
    }

    void Update()
    {

        //Text.text = sentences[currentSentenceNum].TextOutPut();
        TextSwitch();
        if (IsConversation)
        {
            gameController.isCon = true;
            Doctor.SetActive(true);
            TextUI.SetActive(true);
            WhiteBack.SetActive(true);
            if (Input.GetMouseButtonDown(0) && config.configbutton == false)
            {
                OnClick();
            }
        }
        else
        {
            gameController.isCon = false;
            WhiteBack.SetActive(false);
        }

        if (IsDescription)
        {
            gameController.isDes = true;
            Doctor.SetActive(true);
            TextUI.SetActive(true);

            if (sendtext)
            {
                if (!feedin && !feedout)
                {
                    if (sentences.Length - 1 >= currentSentenceNum)
                    {
                        feedin = true;
                        sendtext = false;
                    }
                }

            }

        }
        else
        {
            gameController.isDes = false;
        }

        if (!IsConversation && !IsDescription)
        {
            displaycount++;
            if (displaycount >= 30 && !feedin && !feedout)
            {
                Doctor.SetActive(false);
                TextUI.SetActive(false);
                displaycount = 0;
            }
        }
        if(currentSentenceNum >0) textFeed[currentSentenceNum - 1] = false;
    }

    private void TextSwitch()
    {
        if (preSentenceNum != currentSentenceNum)
        {
            preSentenceNum = currentSentenceNum;
            //Text.text = sentences[currentSentenceNum].TextOutPut();
            StartCoroutine(nowNobel);
        }
    }

    private IEnumerator NovelText()
    {
        int wordCound = 0;
        Text.text = "";
        char newLine = ' ';
        while(wordCound < sentences[currentSentenceNum].TextOutPut().Length)
        {
            if(sentences[currentSentenceNum].TextOutPut()[wordCound] == ' ')
            {
                Text.text += "\n";
            }
            else Text.text += sentences[currentSentenceNum].TextOutPut()[wordCound];
            wordCound++;
            yield return new WaitForSeconds(textWaitTime);
        }
    }
    
    private void OnClick()
    {
        StopCoroutine(nowNobel);
        nowNobel = null; nowNobel = NovelText();
        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (!(mousePosition.x > 0.94f && mousePosition.y > 0.91f ||
            mousePosition.x < 0.19f && mousePosition.y < 0.09f))
        {
            if (!feedin && !feedout)
            {
                if (sentences.Length - 1 >= currentSentenceNum)
                {
                    feedout = true;
                }
            }
        }
    }

    public void LanSwitchText(int lan)
    {
        switch (lan)
        {
            case 0:
                LanguageSwitch.language = LanguageSwitch.Language.Japanese;
                break;
            case 1:
                LanguageSwitch.language = LanguageSwitch.Language.English;
                break;
            case 2:
                LanguageSwitch.language = LanguageSwitch.Language.ChineseHans;
                break;
            case 3:
                LanguageSwitch.language = LanguageSwitch.Language.ChineseHant;
                break;
        }
        //OnClick();
    }
}
