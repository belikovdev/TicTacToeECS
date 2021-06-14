using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class FirstEditorWindow : EditorWindow
{
    string bundleName = string.Empty;
    Texture circleTexture;
    Texture crossTexture;
    Texture backgroundTexture;

    [MenuItem("Window/My Window")]
    public static void ShowWindow()
    {
        GetWindow(typeof(FirstEditorWindow));
    }
     
    private void OnGUI()
    {
        GUILayout.Label("Generate asset bundle", EditorStyles.boldLabel);
        GUILayout.Space(20);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Bundle name", EditorStyles.boldLabel);
        bundleName = EditorGUILayout.TextField(bundleName);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Circle texture", EditorStyles.boldLabel);
        circleTexture = EditorGUILayout.ObjectField(circleTexture, typeof(Texture), false) as Texture;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Cross texture", EditorStyles.boldLabel);
        crossTexture = EditorGUILayout.ObjectField(crossTexture, typeof(Texture), false) as Texture;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Background texture", EditorStyles.boldLabel);
        backgroundTexture = EditorGUILayout.ObjectField(backgroundTexture, typeof(Texture), false) as Texture;
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Build Asset Bundle"))
        {
            if (bundleName.Trim() == string.Empty)
            {
                Debug.LogError("Bundle name cannot be empty, please enter a valid name on the inspector");
            }
            else if (!circleTexture)
            {
                Debug.LogError("No texture found for CIRCLE, please assign a texture on the inspector");
            }
            else if (!crossTexture)
            {
                Debug.LogError("No texture found for CROSS, please assign a texture on the inspector");
            }
            else if (!backgroundTexture)
            {
                Debug.LogError("No texture found for BACKGROUND, please assign a texture on the inspector");
            }
            else
            {
                // use Application.streamingAssetsPath to save asset bundle to Streaming Assets folder as required in test task
                if (!Directory.Exists(Application.streamingAssetsPath))
                {
                    Directory.CreateDirectory(Application.streamingAssetsPath);
                }
                Debug.Log(Application.streamingAssetsPath);

                var circleTexPath = AssetDatabase.GetAssetPath(circleTexture);
                var crossTexPath = AssetDatabase.GetAssetPath(crossTexture);
                var bgTexPath = AssetDatabase.GetAssetPath(backgroundTexture);

                AssetImporter.GetAtPath(circleTexPath).SetAssetBundleNameAndVariant($"belikovassets/{bundleName}", "");
                AssetImporter.GetAtPath(crossTexPath).SetAssetBundleNameAndVariant($"belikovassets/{bundleName}", "");
                AssetImporter.GetAtPath(bgTexPath).SetAssetBundleNameAndVariant($"belikovassets/{bundleName}", "");

                BuildPipeline.BuildAssetBundles(Application.streamingAssetsPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
            }
        }
        EditorGUILayout.EndHorizontal();
    }
}
