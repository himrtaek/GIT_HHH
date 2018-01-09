using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour {

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
            }

            return _instance;
        }
    }

    private int m_iCurGold;
    public void AddGold()
    {
        m_iCurGold++;
        Debug.Log("GetGold! CurGold : " + m_iCurGold.ToString());
    }
}
