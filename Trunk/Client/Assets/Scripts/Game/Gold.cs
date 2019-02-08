using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour {

    public float m_fLifeTime;
    private float m_fElapsedTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_fElapsedTime += Time.deltaTime;
        if(m_fLifeTime <= m_fElapsedTime)
        {
            GameMain.Instance.HeartBreak();
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            GameMain.Instance.AddGold();
            Destroy(gameObject);
        }
    }
}
