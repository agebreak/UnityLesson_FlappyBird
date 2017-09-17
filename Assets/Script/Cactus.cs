using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour {
    public float speed;
    public float rndMax;
    public float rndMin;

	// Use this for initialization
	void Start () {
        // y 높이값을 랜덤으로 결정한. 

        float rnd = Random.Range(rndMin, rndMax);

        Vector3 pos = transform.position;
        pos.y = rnd;
        transform.position = pos;
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.end == true)
            return;

        // 선인장을 움직인다.
        Vector3 pos = GetComponent<Transform>().position;
        pos.x = pos.x + speed * Time.deltaTime;
        GetComponent<Transform>().position = pos;
	}
}
