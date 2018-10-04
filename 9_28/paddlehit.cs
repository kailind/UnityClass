using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paddlehit : MonoBehaviour {
    private AudioSource paddleSound;

	// Use this for initialization
	void Start () {
        paddleSound = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Ball") {
            // play the sound
            paddleSound.Play();
        }
    }
}
