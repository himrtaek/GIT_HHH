using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //StartCoroutine(AdManager.Instance.ShowAd());
        Screen.SetResolution(Screen.width, Screen.width * 1080 / 1920, true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
