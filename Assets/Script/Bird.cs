using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float jumpForce = 5.0f;
    public Vector3 lookRotation;
    public GameObject imageBird;    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {       

        // 마우스 왼쪽 버튼이 클릭되고, y값이 5보다 작을때면
        if (Input.GetMouseButtonDown(0) && transform.position.y < 1.3f)
        {
            // 속도를 리셋한다
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);

            // 위로 힘을 가한다. 
            GetComponent<Rigidbody>().AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
        }

        // 움직일때 새의 방향을 조정한다(연출)
        lookRotation.z = GetComponent<Rigidbody>().velocity.y * 10 + 20;
        Quaternion r = Quaternion.Euler(lookRotation);
        imageBird.transform.rotation = Quaternion.RotateTowards(imageBird.transform.rotation, r, 5.0f);

    }

    public void OnTriggerEnter(Collider other)
    {
        // 선인장과 부딪혔을때
        if(other.tag == "Cactus")
        {
            // 게임 오버
            GetComponent<Rigidbody>().velocity = new Vector3(0, -3, 0);
            lookRotation = new Vector3(0, 0, -90);
            GameManager.instance.GameOver();
        }
        else if(other.tag == "Goal") // 중간을 통과했을때
        {
            // 스코어 증가                        
            GameManager.instance.AddScore();
        }
    }


}