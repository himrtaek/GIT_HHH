using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class BuildManager {

    static string[] SCENES = FindEnabledEditorScenes();
    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }
        return EditorScenes.ToArray();
    }

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

        int sceneSize = EditorBuildSettings.scenes.Length;

        BuildPlayerOptions bpo = new BuildPlayerOptions ();
        bpo.scenes = SCENES;
		bpo.options = BuildOptions.Development | BuildOptions.AllowDebugging;
		bpo.locationPathName = "../Build_Android/output.apk";
		bpo.target = BuildTarget.Android;

		BuildPipeline.BuildPlayer(bpo);
    }

    [MenuItem("Build/Build iOS")]
    static void BuildiOS()
    {
        int sceneSize = EditorBuildSettings.scenes.Length;

        BuildPlayerOptions bpo = new BuildPlayerOptions();
        bpo.scenes = SCENES;
        bpo.options = BuildOptions.Development | BuildOptions.AllowDebugging;
        bpo.locationPathName = "../Build_iOS/output";
        bpo.target = BuildTarget.iOS;

        BuildPipeline.BuildPlayer(bpo);
    }
}
