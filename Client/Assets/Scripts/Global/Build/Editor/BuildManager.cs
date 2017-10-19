using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class BuildManager {

	static void SetKeyStore ()
    {
		PlayerSettings.Android.keystoreName = "KeyStore.keystore";
		PlayerSettings.Android.keystorePass = "rkatjd88";
        PlayerSettings.Android.keyaliasName = "hhh";
        PlayerSettings.Android.keyaliasPass = "rkatjd88";
    }

	[MenuItem("Build/Build Android")]
	static void BuildAndroid()
	{
        SetKeyStore();

		BuildPlayerOptions bpo = new BuildPlayerOptions ();
        bpo.scenes = new string[] { "Assets/Scenes/Splash.unity" };
		bpo.options = BuildOptions.Development | BuildOptions.AllowDebugging;
		bpo.locationPathName = "Build/HHH.apk";
		bpo.target = BuildTarget.Android;

		BuildPipeline.BuildPlayer(bpo);
	}
}
