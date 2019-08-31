using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1Description_2 : MonoBehaviour
{
    [SerializeField] ConversationController conversationController;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            conversationController.IsConversation = true;
            conversationController.preSentenceNum--;
        }
    }
    /*
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            conversationController.feedout = true;
            conversationController.IsDescription = false;
            this.gameObject.SetActive(false);
        }
    }*/
    // Update is called once per frame
    void Update()
    {
        
    }
}
