using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectGameScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
        int sceneSize = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        float posY = 0f;
        for (int i = 1; i < sceneSize; ++i)
        {
            GameObject ButtonPref = Instantiate((GameObject)Resources.Load("Prefabs/SceneButton"));
            ButtonPref.transform.parent = FindObjectOfType<Canvas>().transform;
            ButtonPref.transform.localPosition = new Vector3(0, posY, 0);
            posY -= 50;

            string scenePath = UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = Util.GetFileNameByPath(scenePath);
            ButtonPref.GetComponentInChildren<Text>().text = sceneName;
        }
	}
}
