using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{

    public Ghost Blinky, Pinky, Inky, Clyde;
    public static Game instance;
    public int Lives = 3;
    public byte Level = 0;
    public int Score = 0;
    public Text ScoreText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        Blinky = GameBoard.instance.GameObjects[13, 11].GetComponent<Ghost>();
        Pinky = GameBoard.instance.GameObjects[13, 12].GetComponent<Ghost>();
        Inky = GameBoard.instance.GameObjects[13, 13].GetComponent<Ghost>();
        Clyde = GameBoard.instance.GameObjects[13, 14].GetComponent<Ghost>();

        StartCoroutine(Spawn());

    }

    IEnumerator Spawn()
    {

        yield return new WaitForSeconds(3f);
        Blinky.Chase();
        Blinky.gameObject.transform.position = Blinky.CurrentNode.gameObject.transform.position;

        yield return new WaitForSeconds(3f);
        Pinky.Chase();
        Pinky.gameObject.transform.position = Pinky.CurrentNode.gameObject.transform.position;

        yield return new WaitForSeconds(3f);
        Inky.Chase();
        Inky.gameObject.transform.position = Inky.CurrentNode.gameObject.transform.position;

        yield return new WaitForSeconds(3f);
        Clyde.Chase();
        Clyde.gameObject.transform.position = Clyde.CurrentNode.gameObject.transform.position;
    }



    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "" + Score;

        if (Lives == 0)
        {
            GameOver();
        }
        if (GameBoard.instance.IsLevelComplete())
        {
            LevelComplete();
        }


    }

    public void GameOver()
    {
        //display game over, stop things

    }

    public void LevelComplete()
    {
        //take to next level

        //Debug.Log("Level Complete!");
        Level++;
        PacMan.instance.Respawn();
        GameBoard.instance.ClearGameObjects();
        GameBoard.instance.InstantiatePrefabs();

        Blinky = GameBoard.instance.GameObjects[13, 11].GetComponent<Ghost>();
        Pinky = GameBoard.instance.GameObjects[13, 12].GetComponent<Ghost>();
        Inky = GameBoard.instance.GameObjects[13, 13].GetComponent<Ghost>();
        Clyde = GameBoard.instance.GameObjects[13, 14].GetComponent<Ghost>();

        StartCoroutine(Spawn());

    }



}
