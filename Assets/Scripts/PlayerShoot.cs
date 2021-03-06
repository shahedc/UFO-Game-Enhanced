﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerShoot : MonoBehaviour
{
    public int playerNumber;
    public float speed;
    public GameObject scoreText;
    private static int score;
    private Text scoreTextObject;

    private AudioSource audioSource;
    public AudioClip destroySound;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start () {
        speed = 8f;
        scoreText = GameObject.Find("ScoreText" + playerNumber);
        scoreTextObject = scoreText.GetComponent<Text>();
        score = Convert.ToInt32(scoreTextObject.text.ToString());
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            score++;
            Debug.Log("Player" + playerNumber + " Score:" + score);
            setScoreText();
            var theRock = other.gameObject;
            theRock.SetActive(false);
            Destroy(theRock);

            audioSource.clip = destroySound;
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update () {
        Vector2 position = transform.position;
        position = new Vector2(position.x, position.y + speed * Time.deltaTime);

        transform.position = position;
        
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

        if (transform.position.y > topRight.y)
        {
            Destroy(gameObject);
        }

	}

    void setScoreText()
    {
        try
        {
            scoreTextObject.text = "" + score.ToString();
        }
        catch (NullReferenceException nre)
        {
            Debug.Log("Exception:" + nre.Message);
        }
    }
}
