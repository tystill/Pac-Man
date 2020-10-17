using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan : MonoBehaviour
{

    public float speed = 4f;
    private Vector2 direction = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        Move();
    }

    void CheckInput()
    {
        if (Input.GetKeyDown("j"))
        {
            direction = Vector2.left;

        }
        else if (Input.GetKeyDown("k"))
        {
            direction = Vector2.right;

        }
        else if (Input.GetKeyDown("i"))
        {
            direction = Vector2.up;

        }
        else if (Input.GetKeyDown("m"))
        {
            direction = Vector2.down;

        }
    }

    void Move()
    {
        transform.localPosition += (Vector3)(direction * speed) * Time.deltaTime;
    }

    void UpdateOrientation()
    {

        GetComponent<Animator>().SetFloat("DirX", direction.x);
        GetComponent<Animator>().SetFloat("DirY", direction.y);
    }
}
