﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class PlayerController : MonoBehaviour {
    public int playerNumber;
    public float speed;
    public int health;
    public GameObject healthText;
    private Text healthTextObject;
    private Rigidbody2D rb2d;

    private AudioSource audioSource;
    public AudioClip explosionSound;
    public AudioClip laserSound;

    //    
    public float joysensitivityX = 3F;
    public float joysensitivityY = 3F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -360F;
    public float maximumY = 360F;
    float rotationY = 0F;

    //
    public GameObject laser;
    public GameObject nozzle;
    private bool laserReady = true;
    //
    private Collider2D playerCollider;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start ()
    {
        healthText = GameObject.Find("Health" + playerNumber);
        healthTextObject = healthText.GetComponent<Text>();
        health = Convert.ToInt32(healthTextObject.text.ToString());
        rb2d = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        
    }

	// Update is called once per frame, before rendering a frame
    // most of our update code goes here
	void Update () {      


    }

    // Update is called once per frame, before phsyics
    // for physics code
    void FixedUpdate()
    {
        // move with left joystick or arrow keys
        float moveHorizontal = Input.GetAxis("Joy" + playerNumber + "X");
        float moveVertical = Input.GetAxis("Joy" + playerNumber + "Y");
        //countText.text = "moveHorizontal: " + moveHorizontal;
        //winText.text = "moveVertical: " + moveVertical;

        Vector2 movement = new Vector2(moveHorizontal, 0f); // moveVertical);
        rb2d.AddForce(movement * speed);

        // rotate with right joystick
        /*
        float x = Input.GetAxis("JoyR X");
        float y = Input.GetAxis("JoyR Y");
        if (x > 0 && x < 0.1) x = 0;
        if (y > 0 && y < 0.1) y = 0;
        if (x < 0 && x > -0.1) x = 0;
        if (y < 0 && y > -0.1) y = 0;
        //countText.text = x + "," + y;
        float angle = Mathf.Atan2(x, y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle + 180);
        */
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        // fire lasers with triggers (0 to -1 is right, to +1 is left)
        float triggerMotion = Input.GetAxis("JoyT" + playerNumber);
        bool firePressed = Input.GetButtonDown("Shoot" + playerNumber);      
        if (laserReady && firePressed)
        {
            GameObject laserInstance = (GameObject)Instantiate(laser);
            laserInstance.transform.position = nozzle.transform.position;
            laserReady = false;
            Debug.Log("Shoot: " + playerNumber);

            audioSource.clip = laserSound;
            audioSource.Play();
        }

        if (!firePressed)
        {
            laserReady = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            health = health - 5;
            setHealthText();
            other.gameObject.SetActive(false);

            audioSource.clip = explosionSound;
            audioSource.Play();

        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(other.collider, playerCollider);
        }
    }

    void setHealthText()
    {
        try
        {
            healthTextObject.text = "" + health.ToString();
        }
        catch (NullReferenceException nre)
        {
            Debug.Log("Exception:" + nre.Message);
        }
    }

}
