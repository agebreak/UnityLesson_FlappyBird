using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        // 맨처음 입력받을때, [한번만] 실행된다.
        if (Input.GetMouseButton(0) && ready == true)
        {
            // 1. 새에게 중력을 적용한다. 
            bird.GetComponent<Rigidbody>().useGravity = true;

            // 2. 이제부터 선인장을 생성하기 시작한다. 
            InvokeRepeating("MakeCactus", 1.0f, spawnTime);

            ready = false;
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
        end = true;
        CancelInvoke("MakeCactus");
    }

    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
