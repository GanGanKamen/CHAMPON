using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLanButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
    }
}
