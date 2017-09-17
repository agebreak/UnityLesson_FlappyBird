using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollMove : MonoBehaviour {
    public float speed;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.end)
            return;

        // 오브젝트를 x방향으로 이동시킨다.
        //Vector3 pos = GetComponent<Transform>().position;
        //pos.x = pos.x + 1 * Time.deltaTime;
        //GetComponent<Transform>().position = pos;

        // 오프셋 스크롤을 한다
        Vector2 a = GetComponent<Renderer>().material.mainTextureOffset;
        a.x = a.x + speed * Time.deltaTime;
        GetComponent<Renderer>().material.mainTextureOffset = a;
    }
}
