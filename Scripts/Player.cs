using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float jumpForce = 10f;
    public string currentColor;

    public static int score = 0;
    public static int highScore = 0;

    public Text scoreText;
    public Text highScoreText;

    public Rigidbody2D circle;
    public SpriteRenderer sr;

    public GameObject[] obstical;
    public GameObject colorChanger;

    public Color colorCyan;
    public Color colorYellow;
    public Color colorMagenta;
    public Color colorPink;

    void Start()
    {
        SetRandomColor();
        circle.bodyType = RigidbodyType2D.Kinematic;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            circle.bodyType = RigidbodyType2D.Dynamic;
            circle.velocity = Vector2.up * jumpForce;
        }

        UpdateScore();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Score")
        {
            score++;
            if (score > GetHighScore())
            {
                SetHighScore(score);
            }
            int randomNumber = Random.Range(0, obstical.Length);
            Instantiate(obstical[randomNumber], new Vector2(obstical[randomNumber].transform.position.x, collision.gameObject.transform.position.y + 9f), transform.rotation);
            Destroy(collision.gameObject);
            return;
        }

        if (collision.tag == "ColorChanger")
        {
            SetRandomColor();
            Destroy(collision.gameObject);
            //Instantiate(colorChanger, new Vector2(transform.position.x, transform.position.y + 8f), transform.rotation);
            return;
        }

        if (collision.tag != currentColor)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void SetRandomColor()
    {
        int index = Random.Range(0, 4);

        switch (index)
        {
            case 0:
                currentColor = "Cyan";
                sr.color = colorCyan;
                break;

            case 1:
                currentColor = "Yellow";
                sr.color = colorYellow;
                break;

            case 2:
                currentColor = "Magenta";
                sr.color = colorMagenta;
                break;

            case 3:
                currentColor = "Pink";
                sr.color = colorPink;
                break;
        }
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt("highscore", 0);
    }

    public void SetHighScore(int highScore)
    {
        PlayerPrefs.SetInt("highscore", highScore);
    }

    void UpdateScore()
    {
        scoreText.text = score.ToString();
        highScoreText.text = GetHighScore().ToString();
    }
}

