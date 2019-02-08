using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour {

    const int MaxHeart = 3;
    List<GameObject> m_listHeart = new List<GameObject>();

    private static GameMain _instance;
    public static GameMain Instance
    {
        get
        {
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "GameMain";
                _instance = container.AddComponent(typeof(GameMain)) as GameMain;
                _instance.Init();
            }

            return _instance;
        }
    }

    void Init()
    {
        InitHeart();   
    }

    void InitHeart()
    {
        GameObject heart = Resources.Load("Prefabs/Heart") as GameObject;
        for (int i = 0; i < MaxHeart; ++i)
        {
            Vector3 pos = new Vector3(10 + (7 * i), 2.5f, 0);
            m_listHeart.Insert(i, Instantiate(heart, pos, Quaternion.identity));
        }
    }

    private int m_iCurGold;
    public void AddGold()
    {
        m_iCurGold++;
        Debug.Log("GetGold! CurGold : " + m_iCurGold.ToString());
    }

    public void HeartBreak()
    {
        for (int i = 0; i < m_listHeart.Count; ++i)
        {
            if(m_listHeart[i].activeSelf == true)
            {
                m_listHeart[i].SetActive(false);

                if(i == m_listHeart.Count - 1)
                {
                    GameOver();
                }

                return;
            }
        }
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        UnityEngine.SceneManagement.SceneManager.LoadScene("SelectGame");
    }
}
