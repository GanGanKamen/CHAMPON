using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GururinFall : MonoBehaviour
{

    private Rigidbody2D _rb2d;
    private bool _SEPlay;
    private CriAtomSource _fallSE;

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _fallSE = GameObject.Find("SE_otiru(CriAtomSource)").GetComponent<CriAtomSource>();

        _SEPlay = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //地面と接触したときにSEを止める
        if (other.gameObject.CompareTag("Ground"))
        {
            _fallSE.Stop();
        }
    }

    //非アクティブ時にSEを鳴らす許可をする
    private void OnDisable()
    {
        _SEPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        //落下時にSEを鳴らす
        if (_rb2d.velocity.y < -0.01f && _SEPlay == false)
        {
            _fallSE.Play();
            _SEPlay = true;
        }
    }
}
