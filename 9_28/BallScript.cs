using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    private AudioSource wallsound;
    private AudioSource badwallhit;
    public int playerThatScores = 0;
    public EasyPong mainScript;

    // Use this for initialization
    void Start () {
        AudioSource[] soundsource = GetComponents<AudioSource>();
        wallsound = soundsource[0];
        badwallhit = soundsource[1];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Wall")
        {
            // play the sound
            wallsound.Play();
        }
        else if (collision.collider.tag == "player1wall")
        {

            badwallhit.Play();
            playerThatScores = 1;
            Debug.Log("PLAYER THAT SCORE IS" + playerThatScores);
            mainScript.RegisterScore(playerThatScores);
        }
        else if (collision.collider.tag == "player2wall")
        {

            badwallhit.Play();
            playerThatScores = 2;
            Debug.Log("PLAYER THAT SCORE IS" + playerThatScores);
            mainScript.RegisterScore(playerThatScores);
        }

    }
}
