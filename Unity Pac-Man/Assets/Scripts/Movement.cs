using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{


    private Node.Direction facing;
    private float speed = 3;
    [SerializeField] private Node currentNode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        bool found = false;

        foreach (Node.Direction dir in GameBoard.instance.GetValidDirections(currentNode))
        {
            if (facing == dir)
            {
                found = true;
                switch (facing)
                {

                    case Node.Direction.Up:
                        transform.position += Vector3.up * speed * Time.deltaTime;

                        break;

                    case Node.Direction.Down:

                        transform.position += Vector3.down * speed * Time.deltaTime;

                        break;

                    case Node.Direction.Right:
                        transform.position += Vector3.right * speed * Time.deltaTime;

                        break;

                    case Node.Direction.Left:
                        transform.position += Vector3.left * speed * Time.deltaTime;

                        break;

                }
            }
        }
        if (!found)
        {
            //float t = .5f;
            //transform.position = Vector3.Lerp(transform.position, currentNode.transform.position, t);
            transform.position = currentNode.transform.position;

        }

    }
}
