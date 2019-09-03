using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private GameObject _flag;

    private PlayerMove _playerMove;
    private CriAtomSource _source;

    // Start is called before the first frame update
    void Awake()
    {
        _playerMove = GameObject.Find("Gururin").GetComponent<PlayerMove>();
        _source = GetComponent<CriAtomSource>();

        _source.volume = 0.5f;

        if(_flag != null)
        {
            _flag.SetActive(false);
        }

        //ぐるりんの残機が減った時かつ中間地点に到達していた時にスタート位置を変更
        if(RemainingLife.life != RemainingLife.beforeBossLife && RemainingLife.waypoint)
        {
            RemainingLife.startPos = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (RemainingLife.waypoint == false)
            {
                _source.Play();
            }

            RemainingLife.waypoint = true;

            if (_flag != null)
            {
                _flag.SetActive(true);
            }
        }
    }
}
