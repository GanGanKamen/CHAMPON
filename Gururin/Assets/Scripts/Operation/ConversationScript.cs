using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationScript : MonoBehaviour
{
    [SerializeField] ConversationController conversationController;
    private CanvasGroup canvasGroup;

    public bool IsStart;
    public bool IsDisplay;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        IsStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (conversationController.IsConversation || conversationController.IsDescription)
        {
            this.GetComponent<Text>().text =
                conversationController.sentences[conversationController.currentSentenceNum];

            if (IsStart && conversationController.IsConversation)
            {
                conversationController.feedin = true;
                IsStart = false;
            }
        }

        if (conversationController.feedout)
        {
            conversationController.textFeed
                [conversationController.currentSentenceNum] = true;
            

            if (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= 0.05f;
            }
            else
            {
                if(conversationController.IsConversation)
                {
                    if (conversationController.sentences.Length - 1 > conversationController.currentSentenceNum)
                    {
                        conversationController.currentSentenceNum++;
                        conversationController.feedout = false;
                        conversationController.feedin = true;
                    }
                    else if (conversationController.sentences.Length - 1 == conversationController.currentSentenceNum)
                    {
                        conversationController.feedout = false;
                        
                        conversationController.IsConversation = false;
                    }
                }
                else
                {
                    if (conversationController.sentences.Length - 1 > conversationController.currentSentenceNum)
                    {
                        conversationController.currentSentenceNum++;
                        conversationController.feedout = false;
                    }
                    else if (conversationController.sentences.Length - 1 == conversationController.currentSentenceNum)
                    {
                        conversationController.feedout = false;
                    }
                }
            }
        }

        if (conversationController.feedin)
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += 0.05f;
            }
            else
            {
                conversationController.feedin = false;
            }
        }
        if(!conversationController.IsConversation)
        {
            IsStart = true;
        }
    }
}
