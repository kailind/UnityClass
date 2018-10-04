/* PONG PROJECT, PART I  (Fall 2018)
 * By: Tom Corbett (tcorbett@andrew.cmu.edu)
 * Date Created: 9/14/18
 * Last Update: 9/21/18
 * 
 * Description: This code is the primary script for our Pong Game.  In today's 
 * lesson we placed the objects in the Unity scene, applied scripting to move the
 * ball and paddles, and created a simple "bounce" script to keep the ball in play
 * by determining when it went outside of bounds and "bouncing" it off of the "wall".
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongGame : MonoBehaviour {

    // define ball object
    public GameObject ball;

    // define ball motion properties
    private Vector3 direction;
    public float speed;

    // FIELD variables

    private float FIELD_LEFT = -48.0f;
    private float FIELD_RIGHT = 48.0f;
    private float FIELD_TOP = 23.0f;
    private float FIELD_BOTTOM = -23.0f;

    // define paddles
    public GameObject leftPad;
    public GameObject rightPad;
    public float paddleSpeed;

    private float PAD_MAX_POS;
    private float PAD_MIN_POS;

    // 9-21 NEW CODE //
    private float LEFT_PAD_FRONT = -36.0f;
    private float RIGHT_PAD_FRONT = 36.0f;
    private float PAD_HEIGHT = 10.0f;

	// Use this for initialization
	void Start () {
        PAD_MAX_POS = FIELD_TOP - 5.0f;
        PAD_MIN_POS = FIELD_BOTTOM + 5.0f;

        direction = new Vector3(1.0f, 0.0f, 1.0f);
        direction.Normalize();

	}
	
	// Update is called once per frame
	void Update () {
        MovePaddles(); // moves the paddles

        // MoveBall();
        // CheckBall();

        AdvanceBall();
    }

    void AdvanceBall(){
        // give our ball a direction, and check for collisions.

        Vector3 curPos = ball.transform.position;
        Vector3 offset = direction * speed;
        

    }


    void CheckCollisions(Vector3 startPos, Vector3 offset, float dt){

        // STEP 1: Define our variables, what is endpoint?
        Vector3 endPos = startPos + offset;
        CollisionPoint closest = new CollisionPoint();
        

        // STEP 2:  Find the closest collision

        // case 1: wall (top and bottom)
        if (endPos.z >= FIELD_TOP) {
            // evaluate collision with top
            float uz = (FIELD_TOP - startPos.z) / (offset.z);
            if (uz <= closest.progress) {
                closest = new CollisionPoint((startPos + (offset * uz)), "top", uz);

            }
        } else if (endPos.z <= FIELD_BOTTOM){

        }



        // STEP 3:  Did we find a collision?

        // IF YES:

        //     STEP 4:  PROJECT THE BOUNCE

        //     STEP 5:  RUN CHECK COLLISIONS AGAIN


        // IF NO:

        //     Move to final position


    }







    void CheckBall(){
        Vector3 curPos = ball.transform.position;

        // 9-21 NEW CODE //
        // check paddles
        if (curPos.x <= LEFT_PAD_FRONT) {
            float padPos = leftPad.transform.position.z;
            if ((curPos.z >= padPos - (PAD_HEIGHT * 0.5f)) && (curPos.z <= padPos + (PAD_HEIGHT * 0.5f))) {
                BounceBall(curPos, "leftpad");
            }
            print("left bounce");
        } else if (curPos.x >= RIGHT_PAD_FRONT) {
            float padPos = rightPad.transform.position.z;
            if ((curPos.z >= padPos - (PAD_HEIGHT * 0.5f)) && (curPos.z <= padPos + (PAD_HEIGHT * 0.5f)))
            {
                BounceBall(curPos, "rightpad");
            }
            print("right bounce");
        }

        // check top & bottom
        if (curPos.z >= FIELD_TOP) {
            BounceBall(curPos, "top");
        } else if (curPos.z <= FIELD_BOTTOM) {
            BounceBall(curPos, "bottom");
        }

        // check left & right
        if (curPos.x >= FIELD_RIGHT){
            BounceBall(curPos, "right");
        } else if (curPos.x <= FIELD_LEFT){
            BounceBall(curPos, "left");
        }

    }

    void BounceBall(Vector3 bouncePos, string bounceObj) {

        switch(bounceObj) {

            case "top":
                //top code
                bouncePos.z = FIELD_TOP - (bouncePos.z - FIELD_TOP);
                ball.transform.position = bouncePos;
                direction.z = -direction.z;
                break;
            case "bottom":
                // bottom code
                bouncePos.z = FIELD_BOTTOM - (bouncePos.z - FIELD_BOTTOM);
                ball.transform.position = bouncePos;
                direction.z = -direction.z;
                break;
            case "right":
                // right code
                bouncePos.x = (2 * FIELD_RIGHT) - bouncePos.x;
                ball.transform.position = bouncePos;
                direction.x = -direction.x;
                break;
            case "left":
                // left code
                bouncePos.x = (2 * FIELD_LEFT) - bouncePos.x;
                ball.transform.position = bouncePos;
                direction.x = -direction.x;
                break;

                // 9-21 NEW CODE //
            case "rightpad":
                // rightPad code
                bouncePos.x = (2 * RIGHT_PAD_FRONT) - bouncePos.x;
                ball.transform.position = bouncePos;
                direction.x = -direction.x;
                break;
            case "leftpad":
                // right code
                bouncePos.x = (2 * LEFT_PAD_FRONT) - bouncePos.x;
                ball.transform.position = bouncePos;
                direction.x = -direction.x;
                break;

            default:

                break;
        }

    }

    void MoveBall(){
        // move the ball to the new position
        ball.transform.position += (direction * speed);
        //  ball.transform.position = ball.transform.position + (direction * speed);
    }

    void MovePaddles() {
        // check for input, and move the paddles accordingly

        if (Input.GetKey("w")){
            PaddleShift(leftPad, "up");
        }
        if (Input.GetKey("s")){
            PaddleShift(leftPad, "down");
        }
        if (Input.GetKey("up")){
            PaddleShift(rightPad, "up");
        }
        if (Input.GetKey("down")){
            PaddleShift(rightPad, "down");
        }

        EvalPaddles(leftPad);
        EvalPaddles(rightPad);

    }

    void PaddleShift(GameObject thisPad, string thisDir){
        Vector3 padMove = new Vector3();

        if (thisDir == "up") {
            padMove.z = paddleSpeed;
        } else if (thisDir == "down") {
            padMove.z = -paddleSpeed;

        } else {
            return;
        }

        thisPad.transform.Translate(padMove, Space.World);

    }

    void EvalPaddles(GameObject thisPaddle) {
        Vector3 padPos = thisPaddle.transform.position;

        if (padPos.z > PAD_MAX_POS) {
            padPos.z = PAD_MAX_POS;
            thisPaddle.transform.position = padPos;
        } else if (padPos.z < PAD_MIN_POS) {
            padPos.z = PAD_MIN_POS;
            thisPaddle.transform.position = padPos;
        }


    }
}

public class CollisionPoint {
    public Vector3 point;
    public string type;
    public float progress;
    public bool found;


    public CollisionPoint() {
        point = new Vector3();
        type = "null";
        progress = 1.0f;
        found = false;
    }

    public CollisionPoint(Vector3 newPoint, string newType, float newProgress) {
        point = newPoint;
        type = newType;
        progress = newProgress;
        found = true;
    }

}
