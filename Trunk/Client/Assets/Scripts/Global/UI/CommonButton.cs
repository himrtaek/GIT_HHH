using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeScene()
    {
        Text textComp = GetComponentInChildren<UnityEngine.UI.Text>();
        string sceneName = textComp.text;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
