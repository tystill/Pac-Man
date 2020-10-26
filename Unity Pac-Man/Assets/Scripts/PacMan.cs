using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PacMan : MonoBehaviour
{

    private float speed = 3f;
    private Vector2 direction = Vector2.zero;
    [SerializeField] private Node currentNode;
    //private Node destinationNode;
    public Node.Direction facing;
    private Node.Direction queuedDirection;
    private bool startedMoving = false;
    private bool inNode = false;
    private Node startingNode;
    public static PacMan instance;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        startingNode = GameBoard.instance.GameObjects[22, 13].GetComponent<Node>();
        currentNode = startingNode;
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
        QueuedDirection();
        int i = 0;
        foreach (Node.Direction dir in GameBoard.instance.GetValidDirections(currentNode))
        {


            if (((Input.GetKeyDown("k") /*|| Input.GetAxis("horizontal") > 0*/) && (dir == Node.Direction.Right) && facing != Node.Direction.Right)
                ||(queuedDirection == Node.Direction.Right) && (dir == Node.Direction.Right))
            {
                Debug.Log("pressed right");

                if (!startedMoving)
                {
                    facing = Node.Direction.Right;
                    startedMoving = true;
                }
                if (inNode && queuedDirection != Node.Direction.None)
                {
                    facing = queuedDirection;
                    //transform.position = GameBoard.instance.GetNodeInDirection(currentNode,dir).transform.position;
                    transform.position = currentNode.transform.position;
                }
                //currentNode = currentNode.neighbors[i];
                transform.localRotation = Quaternion.Euler(0, 0, 360);
                queuedDirection = Node.Direction.None;

            }
            else if (((Input.GetKeyDown("j") /*|| Input.GetAxis("horizontal") < 0*/) && (dir == Node.Direction.Left) && facing != Node.Direction.Left)
                            || (queuedDirection == Node.Direction.Left) && (dir == Node.Direction.Left))
            {
                Debug.Log("pressed left");
                if (!startedMoving)
                {
                    facing = Node.Direction.Left;
                    startedMoving = true;
                }
                if (inNode && queuedDirection != Node.Direction.None)
                {
                    facing = queuedDirection;
                    transform.position = currentNode.transform.position;


                }
                //currentNode = currentNode.neighbors[i];
                transform.localRotation = Quaternion.Euler(0, 0, 180);
                queuedDirection = Node.Direction.None;


            }
            else if (((Input.GetKeyDown("i") /*|| Input.GetAxis("vertical") > 0*/) && (dir == Node.Direction.Up) && facing != Node.Direction.Up)
                            || (queuedDirection == Node.Direction.Up) && (dir == Node.Direction.Up))
            {
                Debug.Log("pressed up");

                if (inNode && queuedDirection != Node.Direction.None)
                {
                    facing = queuedDirection;
                    transform.position = currentNode.transform.position;

                }

                //currentNode = currentNode.neighbors[i];
                transform.localRotation = Quaternion.Euler(0, 0, 90);
                queuedDirection = Node.Direction.None;


            }
            else if (((Input.GetKeyDown("m") /*|| Input.GetAxis("vertical") < 0*/) && (dir == Node.Direction.Down) && facing != Node.Direction.Down)
                            || (queuedDirection == Node.Direction.Down) && (dir == Node.Direction.Down))
            {
                Debug.Log("pressed down");

                if (inNode && queuedDirection != Node.Direction.None)
                {
                    facing = queuedDirection;
                    transform.position = currentNode.transform.position;

                }
                //currentNode = currentNode.neighbors[i];
                transform.localRotation = Quaternion.Euler(0, 0, 270);
                queuedDirection = Node.Direction.None;


            }
            i++;
        }
    }


    void QueuedDirection()
    {
        if (Input.GetKeyDown("k") /*|| Input.GetAxis("horizontal") > 0*/ )
        {
            queuedDirection = Node.Direction.Right;


        }
        else if (Input.GetKeyDown("j") /*|| Input.GetAxis("horizontal") < 0*/)
        {
            
            queuedDirection = Node.Direction.Left;
         
        }
        else if (Input.GetKeyDown("i") /*|| Input.GetAxis("vertical") > 0*/)
        {
            
            queuedDirection = Node.Direction.Up;

        }
        else if (Input.GetKeyDown("m") /*|| Input.GetAxis("vertical") < 0*/)
        {
            queuedDirection = Node.Direction.Down;

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
            Node tempNode = collision.gameObject.GetComponent<Node>();

            inNode = true;
            if (GameBoard.instance.GetValidDirections(tempNode).Contains(queuedDirection))
            {
                facing = queuedDirection;
            }
            if (tempNode.Type == Node.NodeType.Portal)
            {
                if (facing == tempNode.PortalDirection)
                {
                    Debug.Log("Found Portal");
                    transform.position = tempNode.PortalDestination.transform.position;
                    currentNode = tempNode.PortalDestination;
                }

            }

            else
            {
                currentNode = collision.gameObject.GetComponent<Node>();

                //can put hide pellet and increase score here
                SpriteRenderer sprite = collision.gameObject.GetComponent<SpriteRenderer>();
                if (sprite)
                {
                    
                    sprite.enabled = false;
                    Game.instance.Score += currentNode.Type == Node.NodeType.Pellet? 10:50;
                    if ((currentNode.Type == Node.NodeType.PowerUp) || (currentNode.Type == Node.NodeType.Pellet))
                    {
                        GameBoard.instance.pelletCount--;
                    }

                    if(currentNode.Type == Node.NodeType.PowerUp)
                    {
                        Debug.Log("found powerUp");
                        if(Game.instance.Blinky.CurrentState != Ghost.State.Idle)
                        {
                            Game.instance.Blinky.BecomeFrightened();

                        }
                        if (Game.instance.Pinky.CurrentState != Ghost.State.Idle)
                        {
                            Game.instance.Pinky.BecomeFrightened();

                        }
                        if (Game.instance.Inky.CurrentState != Ghost.State.Idle)
                        {
                            Game.instance.Inky.BecomeFrightened();

                        }
                        if (Game.instance.Clyde.CurrentState != Ghost.State.Idle)
                        {
                            Game.instance.Clyde.BecomeFrightened();

                        }
                    }
                }
                currentNode.Type = Node.NodeType.Invisible;

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
            else if(hit.CurrentState == Ghost.State.Frightened)
            {
                hit.Die();
            }
        }

    }

    private void Die(){
        Debug.Log("Got into Die");
        Game.instance.Lives--;
        //play die animation



        //respawn
        Respawn();

        }

    public void Respawn()
    {
        transform.position = startingNode.transform.position;
        startedMoving = false;
        facing = Node.Direction.Right;

        transform.localRotation = Quaternion.Euler(0, 0, 360);
    }


}