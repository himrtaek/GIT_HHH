using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spanwer : MonoBehaviour {

    public float m_fSpawnInterval;
    private float m_fElapsedTime;

    public GameObject m_gSpawnItem;

    public Vector3[] m_arrSpawnPos;
    private int m_iSpawnPosIndex = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        m_fElapsedTime += Time.deltaTime;
        if(m_fSpawnInterval <= m_fElapsedTime)
        {
            Spawn();
        }
        else if (GameObject.FindWithTag("Gold") == null)
        {
            Spawn();
        }
	}

    void Spawn()
    {
        Instantiate(m_gSpawnItem, m_arrSpawnPos[m_iSpawnPosIndex++], Quaternion.identity);
        if (m_iSpawnPosIndex >= m_arrSpawnPos.Length)
        {
            m_iSpawnPosIndex = 0;
        }

        m_fElapsedTime -= m_fSpawnInterval;
    }
}
