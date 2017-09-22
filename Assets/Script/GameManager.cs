using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject cactus; // 생성할 선인장 프리팹
    public float spawnTime = 10.0f; // 선인장을 생성할 시간 간격
    public int score = 0; // 게임 점수
    public static GameManager instance = null; // 싱글톤 변수를 만든다
    public bool ready = true;
    public bool end = false;
    public GameObject bird;
    public Text scoreText;

    // 재생할 오디오 클립 
    public AudioClip death;
    public AudioClip goal;

    public GameObject readyImg;
    public GameObject readyTab;
    public GameObject gameOverGUI;
    public TextMesh gameOver_Score;
    public TextMesh gameOver_HighScore;
    public GameObject gameOver_New;

    public void Awake()
    {
        // 싱글톤 변수에 자신을 등록한다.
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        //InvokeRepeating("MakeCactus", 1.0f, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        // 맨처음 입력받을때, [한번만] 실행된다. => 게임 시작
        if (Input.GetMouseButton(0) && ready == true)
        {
            // 1. 새에게 중력을 적용한다. 
            bird.GetComponent<Rigidbody>().useGravity = true;

            // 2. 이제부터 선인장을 생성하기 시작한다. 
            InvokeRepeating("MakeCactus", 1.0f, spawnTime);

            ready = false;

            //readyImg.SetActive(false);
            //readyTab.SetActive(false);

            iTween.ColorTo(readyImg, new Color(1, 1, 1, 0), 0.5f);
            iTween.ColorTo(readyTab, new Color(1, 1, 1, 0), 0.5f);
        }

        // 게임을 끝났으면, 터치하면 재시작한다. 
        if(end && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);
        }
    }

    void MakeCactus()
    {
        Instantiate(cactus);
    }

    // 1. 새가 부딪혔을때 GameOver를 호출한다. 
    // 2. GameOver 상태가 되면 선인장이 멈춘다. 
    // 3. GameOVer 상태가 되면 배경 스크롤이 멈춘다.

    public void GameOver()
    {
        if (end)
            return;

        end = true;
        CancelInvoke("MakeCactus");

        //GetComponent<AudioSource>().Play();
        AudioSource.PlayClipAtPoint(death, new Vector3(0, 0, 0));

        // 게임 오버 GUI를 표시한다. 
        gameOverGUI.SetActive(true);
        iTween.MoveFrom(gameOverGUI, new Vector3(0, -3, 0), 1.0f);

        // 점수를 표시한다. 
        gameOver_Score.text = score.ToString();
        int highScore = PlayerPrefs.GetInt("HighScore");

        // 하이스코어 표시 
        gameOver_HighScore.text = highScore.ToString();
        if(score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            gameOver_New.SetActive(true);
        }
        else
        {
            gameOver_New.SetActive(false);
        }
    }

    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();

        AudioSource.PlayClipAtPoint(goal, Vector3.zero);
    }
}
