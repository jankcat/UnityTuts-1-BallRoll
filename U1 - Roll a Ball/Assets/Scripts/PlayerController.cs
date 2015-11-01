using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text scoreText;
    public Text winText;
    public Text timerText;
    public Text titleText;
    public Button startButton;
    public GameObject gameMusic;
    public GameObject boopSound;
    public GameObject introMusic;
    public GameObject winSound;

    private bool gameOn;
    private Rigidbody rigidBody;
    private List<GameObject> deadPickUpItems;
    private float startTime;
    private float endTime;

    void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        winText.text = "You Win!";
        var pickups = GameObject.FindGameObjectsWithTag("PickUpItem");
        scoreText.text = string.Format("Remaining: {0}", pickups.Length);
        deadPickUpItems = new List<GameObject>();
    }

    void FixedUpdate()
    {
        if (!gameOn) return;
        float moveHoriz = Input.GetAxis("Horizontal");
        float moveVert = Input.GetAxis("Vertical");
        var movement = new Vector3(moveHoriz, 0.0f, moveVert);
        rigidBody.AddForce(movement * speed);
    }

    void Update()
    {
        UpdateTimerText();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUpItem"))
        {
            boopSound.GetComponent<AudioSource>().Play();
            other.gameObject.SetActive(false);
            deadPickUpItems.Add(other.gameObject);
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        var pickups = GameObject.FindGameObjectsWithTag("PickUpItem");
        scoreText.text = string.Format("Remaining: {0}", pickups.Length);
        if (pickups.Length <= 0)
        {
            boopSound.GetComponent<AudioSource>().Stop();
            winSound.GetComponent<AudioSource>().Play();
            gameOn = false;
            endTime = Time.time - startTime;
            winText.gameObject.SetActive(true);
            gameMusic.SetActive(false);
            introMusic.SetActive(true);
            introMusic.GetComponent<AudioSource>().Stop();
            introMusic.GetComponent<AudioSource>().PlayDelayed(1.5f);
        }
    }

    void UpdateTimerText()
    {
        var elapsedTime = (gameOn) ? Time.time - startTime : endTime;
        timerText.text = string.Format("Time: {0}", (int)elapsedTime);
    }
    
    public void StartGame()
    {
        titleText.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
        gameOn = true;
        startTime = Time.time;
        endTime = 0.0f;
        UpdateScoreText();
        UpdateTimerText();
        deadPickUpItems = new List<GameObject>();
        introMusic.SetActive(false);
        gameMusic.SetActive(true);
    }

    public void ResetGame()
    {
        gameOn = false;
        winText.gameObject.SetActive(false);
        transform.position = new Vector3(0.0f, 0.5f, 0.0f);
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        gameMusic.SetActive(false);
        introMusic.SetActive(true);

        foreach (var item in deadPickUpItems)
        {
            item.SetActive(true);
        }

        titleText.gameObject.SetActive(true);
        startButton.gameObject.SetActive(true);

        var pickups = GameObject.FindGameObjectsWithTag("PickUpItem");
        scoreText.text = string.Format("Remaining: {0}", pickups.Length);
    }
}
