using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindArea : MonoBehaviour
{

    private bool _sourceStart;
    private CriAtomSource _source;
    private string aisacControllerName = "Plopeller_wind";
    public float currentControlValue = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<CriAtomSource>();
        _sourceStart = false;
        //SEの音量
        _source.volume = 0.2f;
        currentControlValue = 0.0f;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _sourceStart == false)
        {
            _source.Play();
            _sourceStart = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _sourceStart)
        {
            _sourceStart = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_sourceStart)
        {
            currentControlValue += 0.005f;
            if (currentControlValue >= 1.0f)
            {
                currentControlValue = 1.0f;
            }
        }
        else if (_sourceStart == false)
        {
            currentControlValue -= 0.01f;
            if (currentControlValue <= 0.0f)
            {
                currentControlValue = 0.0f;
                _source.Stop();
            }
        }
    }
}
