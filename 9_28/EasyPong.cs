using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// state values
public enum GameState { none, playing, getReady, gameOver };


public class EasyPong : MonoBehaviour {
    public GameObject ballobj;
    private Rigidbody ball;
    public float ballSpeed = 1.0f;
    private Renderer ballRender;
    public Text getReadyText;
    public Text gameOverText;

    // define paddles
    public GameObject leftPad;
    public GameObject rightPad;
    public float paddleSpeed;

    private float PAD_MAX_POS;
    private float PAD_MIN_POS;
    private float FIELD_TOP = 23.0f;
    private float FIELD_BOTTOM = -23.0f;

    // 9-21 NEW CODE //
    private float LEFT_PAD_FRONT = -36.0f;
    private float RIGHT_PAD_FRONT = 36.0f;
    private float PAD_HEIGHT = 10.0f;

    // define game state variable
    private GameState state = GameState.none;
    private int player1score = 0;
    private int player2score = 0;
    private int timeRunScore = 0;

    //define player score and display when players score a point
    public Text playerScore;




    // Use this for initialization
    void Start () {
        getReadyText.GetComponent<Text>().enabled = false;
        gameOverText.GetComponent<Text>().enabled = false;
        playerScore.GetComponent<Text>().enabled = false;

        PAD_MAX_POS = FIELD_TOP - 5.0f;
        PAD_MIN_POS = FIELD_BOTTOM + 5.0f;

        ball = ballobj.GetComponent<Rigidbody>();
        ballRender = ballobj.GetComponent<Renderer>();

        InitGame();

	}
	
	// Update is called once per frame
	void Update () {
        MovePaddles();
	}

    void InitGame(){
        player1score = 0;
        player2score = 0;
        ResetBall();
    }

    void ResetBall(){
        state = GameState.getReady;

        ball.isKinematic = true;
        ball.transform.position = Vector3.zero;


        StartCoroutine(ReadyBlink());

    }

    void StartBall(){
        ball.isKinematic = false;
        //Randomize direction
        Vector3 ballDirection = new Vector3(Random.Range(1.0f, 3.0f), 0, Random.Range(-3.0f, -1.0f));

        //Vector3 ballDirection = new Vector3(1, 0, -1);
        ball.AddForce(ballDirection * ballSpeed);

        //Add force to the ball in each round
        ballSpeed += 300;
        Debug.Log("Ball speed is" + ballSpeed);
        state = GameState.playing;
    }

    public void RegisterScore(int playerWall){

        timeRunScore += 1;
        Debug.Log("Player " + playerWall + " scored a point.");
        Debug.Log("TIME RUN SCORE IS " + timeRunScore);
        if (playerWall==1)
        {

            player1score += 1;
            Debug.Log("player1score" + player1score);
            playerScore.GetComponent<Text>().enabled = true;
            playerScore.GetComponent<Text>().text = "Score " +player1score+":"+player2score;

            //StartCoroutine(DisplayScore());

            if (player1score >= 3)
            {
                Debug.Log("Game Over!! Player 1 won");
                gameOverText.GetComponent<Text>().enabled = true;
                gameOverText.GetComponent<Text>().text = "Game over!!Player 1 has won!";
                state = GameState.gameOver;
                StopGame();
                //InitGame();
            }

        }
        else if(playerWall==2)
        {
     
            player2score += 1;
               
            Debug.Log("player2score" + player2score);
            playerScore.GetComponent<Text>().enabled = true;
            playerScore.GetComponent<Text>().text = "Score " + player1score + ":" + player2score;
            if (player2score >= 3)
            {
                gameOverText.GetComponent<Text>().enabled = true;
                gameOverText.GetComponent<Text>().text = "Game over!!Player 2 has won!";
                Debug.Log("Game over!!Player 2 won");
                state = GameState.gameOver;
                StopGame();
                //InitGame();
            }


        }

        // round is over, reset the ball
        ResetBall();

    }

    void MovePaddles()
    {
        // check for input, and move the paddles accordingly

        if (Input.GetKey("w"))
        {
            PaddleShift(leftPad, "up");
        }
        if (Input.GetKey("s"))
        {
            PaddleShift(leftPad, "down");
        }
        if (Input.GetKey("up"))
        {
            PaddleShift(rightPad, "up");
        }
        if (Input.GetKey("down"))
        {
            PaddleShift(rightPad, "down");
        }

        //EvalPaddles(leftPad);
        //EvalPaddles(rightPad);

    }

    void PaddleShift(GameObject thisPad, string thisDir)
    {
        Vector3 rpadPos = thisPad.transform.position; // where is my paddle
        float xPos = rpadPos.x;
        float yPos = rpadPos.y;
        float zPos = rpadPos.z;
        if (thisDir == "up")
        {
            zPos = rpadPos.z + paddleSpeed;
        }
        else if (thisDir == "down")
        {
            zPos = rpadPos.z - paddleSpeed;
        }


        rpadPos = new Vector3(xPos, yPos, Mathf.Clamp(zPos, -20.0f, 20.0f));
        thisPad.transform.position = rpadPos;

    }

    void EvalPaddles(GameObject thisPaddle)
    {
        Vector3 padPos = thisPaddle.transform.position;

        if (padPos.z > PAD_MAX_POS)
        {
            padPos.z = PAD_MAX_POS;
            thisPaddle.transform.position = padPos;
        }
        else if (padPos.z < PAD_MIN_POS)
        {
            padPos.z = PAD_MIN_POS;
            thisPaddle.transform.position = padPos;
        }


    }


    //public IEnumerable DisplayScore(){

    //    Debug.Log("GAME STATE IS PLAYING");
    //    playerScore.GetComponent<Text>().enabled = true;
    //    yield return new WaitForSeconds(1.0f);
    //    playerScore.GetComponent<Text>().enabled = false;
    //    getReadyText.GetComponent<Text>().enabled = true;



    //}

    void StopGame(){
        Time.timeScale = 0;//freeze game
    }



    public IEnumerator ReadyBlink(){

        playerScore.GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(1.0f);
        playerScore.GetComponent<Text>().enabled = false;
        getReadyText.GetComponent<Text>().enabled = true;
        getReadyText.GetComponent<Text>().enabled = true;


        for (int i = 0; i < 3; i++)
        {
                // turn the ball off
                ballRender.enabled = false;
                yield return new WaitForSeconds(0.5f);

                // turn the ball on
                ballRender.enabled = true;
                yield return new WaitForSeconds(0.5f);
         }

         getReadyText.GetComponent<Text>().enabled = false;

         StartBall();



    }


    /* 
     * 
     * OLD PADDLE MOVING SCRIPT
     * 
Vector3 rpadPos = rightPaddle.transform.position; // where is my paddle

float xPos = rpadPos.x;
float yPos = rpadPos.y;
float zPos = rpadPos.z + (Input.GetAxisRaw("Vertical") * paddleSpeed);

rpadPos = new Vector3(xPos, yPos, Mathf.Clamp(zPos, -20.0f, 20.0f));
rightPaddle.transform.position = rpadPos;

*/

}
