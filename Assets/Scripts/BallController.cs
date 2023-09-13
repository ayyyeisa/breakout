using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class BallController : MonoBehaviour
{
    public int Lives;
    public bool HasBeenLaunched;
    public GameObject Paddle;
    public GameManager GM;
    void Start()
    {
        Lives = 3;
    }

    private void Update()
    {
        if (HasBeenLaunched == false)
        {
            transform.position = new Vector3(Paddle.transform.position.x, Paddle.transform.position.y + 0.75f, Paddle.transform.position.z);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "KillBox")
        {
            Lives--;
            HasBeenLaunched = false;
            if(Lives <= 0)
            {
                GM.LoseGame();
            }
        }
    }

    public void Launch()
    {
        if(HasBeenLaunched == false)
        {
            HasBeenLaunched = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 10f);
        }
    }
}
