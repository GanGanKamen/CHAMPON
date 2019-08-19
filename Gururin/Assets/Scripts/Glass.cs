using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{

    private Rigidbody2D _rb2d;
    public GameObject balloonPrefab;
    public float dropDistance;
    public bool collision;

    // Start is called before the first frame update
    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jump"))
        {
            collision = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Jump"))
        {
            collision = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_rb2d.velocity.y < -8.0f)
        {
            Debug.Log(_rb2d.velocity.y);
        }

        if (collision && _rb2d.velocity.y < dropDistance)
        {
            var pos = transform.position;
            var balloon = Instantiate(balloonPrefab);
            balloon.transform.position = new Vector2(pos.x, pos.y + 0.3f);
            gameObject.SetActive(false);
        }
    }
}
