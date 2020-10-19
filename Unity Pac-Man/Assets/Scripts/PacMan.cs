using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.XR.WSA.Input;

public class PacMan : MonoBehaviour
{

    private float speed = 3;
    private Vector2 direction = Vector2.zero;
    [SerializeField] private Node currentNode;
    private Node destinationNode;
    private Node.Direction facing;
    private Node.Direction queuedDirection;
    private bool startedMoving = false;
    private bool inNode = false;


    // Start is called before the first frame update
    void Start()
    {
        currentNode = GameBoard.instance.GameObjects[22, 13].GetComponent<Node>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeDirection();

        if (startedMoving)
        {
            Move();
        }
    }

    void ChangeDirection()
    {
        int i = 0;
        foreach (Node.Direction dir in GameBoard.instance.GetValidDirections(currentNode))
        {


            if ((Input.GetKeyDown("k") /*|| Input.GetAxis("horizontal") > 0*/) && (dir == Node.Direction.Right) && facing != Node.Direction.Right)
            {
                Debug.Log("pressed right");
                queuedDirection = Node.Direction.Right;
                if (!startedMoving)
                {
                    facing = Node.Direction.Right;
                    startedMoving = true;
                }
                if (inNode)
                {
                    facing = queuedDirection;
                }
                //currentNode = currentNode.neighbors[i];
                transform.localRotation = Quaternion.Euler(0, 0, 360);


            }
            else if ((Input.GetKeyDown("j") /*|| Input.GetAxis("horizontal") < 0*/) && (dir == Node.Direction.Left) && facing != Node.Direction.Left)
            {
                Debug.Log("pressed left");
                queuedDirection = Node.Direction.Left;
                if (!startedMoving)
                {
                    facing = Node.Direction.Left;
                    startedMoving = true;
                }
                if (inNode)
                {
                    facing = queuedDirection;
                }
                //currentNode = currentNode.neighbors[i];
                transform.localRotation = Quaternion.Euler(0, 0, 180);


            }
            else if ((Input.GetKeyDown("i") /*|| Input.GetAxis("vertical") > 0*/) && (dir == Node.Direction.Up) && facing != Node.Direction.Up)
            {
                Debug.Log("pressed up");
                queuedDirection = Node.Direction.Up;

                if (inNode)
                {
                    facing = queuedDirection;
                }

                //currentNode = currentNode.neighbors[i];
                transform.localRotation = Quaternion.Euler(0, 0, 90);


            }
            else if ((Input.GetKeyDown("m") /*|| Input.GetAxis("vertical") < 0*/) && (dir == Node.Direction.Down) && facing != Node.Direction.Down)
            {
                Debug.Log("pressed down");
                queuedDirection = Node.Direction.Down;

                if (inNode)
                {
                    facing = queuedDirection;
                }
                //currentNode = currentNode.neighbors[i];
                transform.localRotation = Quaternion.Euler(0, 0, 270);


            }
            i++;
        }
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



    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Found Collision");
        if (collision.gameObject.tag == "Node")
        {
            //Debug.Log("Found Node");
            inNode = true;
            facing = queuedDirection;
            Portal portal = collision.gameObject.GetComponent<Portal>();
            if (portal)
            {
                if (facing == portal.direction)
                {
                    Debug.Log("Found Portal");
                    transform.position = portal.Destination.transform.position;
                    currentNode = portal.Destination.GetComponent<Node>();
                }

            }

            else
            {
                currentNode = collision.gameObject.GetComponent<Node>();

                //can put hide pellet and increase score here
                collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;

            }
            GameBoard.instance.PacRow = currentNode.row;
            GameBoard.instance.PacCol = currentNode.column;


        }

        if (collision.gameObject.tag == "Ghost")
        {
            Ghost hit = collision.gameObject.GetComponent<Ghost>();
            if (hit.CurrentState == Ghost.State.Chase || hit.CurrentState == Ghost.State.Scatter)
            {
                Die();
            }
        }

    }

    private void Die(){

        }
        



}