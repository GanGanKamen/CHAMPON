using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallFloor : MonoBehaviour
{
    [SerializeField] private float endurance;
    [SerializeField] private GameObject[] targets;
    [SerializeField] private float shakeSpeed;
    [SerializeField] private float shakeWidth;
    [SerializeField] private float fallSpeed;

    private float[] startPosx;
    private float[] direction;

    public enum Status
    {
        Normal,
        Shake,
        Fall
    }
    public Status status;
    // Start is called before the first frame update
    void Start()
    {
        status = Status.Normal;
        startPosx = new float[targets.Length];
        direction = new float[targets.Length];
        for (int i = 0; i < targets.Length; i++)
        {
            startPosx[i] = targets[i].transform.localPosition.x;
            direction[i] = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (status)
        {
            case Status.Normal:
                break;
            case Status.Shake:
                Shaking(shakeSpeed, shakeWidth);
                break;
            case Status.Fall:
                Falling(fallSpeed);
                break;
        }

        if(endurance <= 0)
        {
            status = Status.Fall;
        }
    }

    private void Shaking(float speed, float width)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            if (direction[i] == -1)
            {
                //targets[i].transform.Translate(-speed*Time.deltaTime, 0, 0);
                targets[i].transform.localPosition -= new Vector3(speed * Time.deltaTime, 0, 0);
                if (targets[i].transform.localPosition.x <= startPosx[i] - width)
                {
                    direction[i] = 1;
                }
            }
            else if (direction[i] == 1)
            {
                //targets[i].transform.Translate(speed * Time.deltaTime, 0, 0);
                targets[i].transform.localPosition += new Vector3(speed * Time.deltaTime, 0, 0);
                if (targets[i].transform.localPosition.x >= startPosx[i] + width)
                {
                    direction[i] = -1;
                }
            }
        }

        endurance -= Time.deltaTime;
    }

    private void Falling(float speed)
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].transform.localPosition -= new Vector3(0, speed * Time.deltaTime, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && status == Status.Normal)
        {
            status = Status.Shake;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Died") && status == Status.Fall)
        {
            Destroy(gameObject);
        }
    }
}
