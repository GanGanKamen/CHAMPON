using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteHoleSE : MonoBehaviour
{

    private CriAtomSource _source;
    private bool _volume;

    // Start is called before the first frame update
    void Start()
    {
        _source = GetComponent<CriAtomSource>();
        _source.volume = 0.2f;
        _volume = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _source.volume += 0.01f;
            if(_source.volume >= 1.0f)
            {
                _source.volume = 1.0f;
            }
            _volume = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _volume = true;
        }
    }

    private void Update()
    {
        if (_volume)
        {
            _source.volume -= 0.01f;
            if (_source.volume <= 0.2f)
            {
                _source.volume = 0.2f;
            }
        }
    }
}
