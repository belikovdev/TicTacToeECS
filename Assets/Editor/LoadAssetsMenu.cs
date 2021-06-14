using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class LoadAssetsMenu : EditorWindow
{
    DefaultAsset assetBundle;

    [MenuItem("Belikovdev/Load Asset Bundle")]
    public static void ShowWindow()
    {
        GetWindow(typeof(LoadAssetsMenu));
    }

    private void OnGUI()
    {
        GUILayout.Label("Load asset bundle", EditorStyles.boldLabel);
        GUILayout.Space(20);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Asset Bundle", EditorStyles.boldLabel);
        assetBundle = EditorGUILayout.ObjectField(assetBundle, typeof(DefaultAsset), false) as DefaultAsset;
        EditorGUILayout.EndHorizontal();

        if(GUILayout.Button("Load"))
        {
            if (assetBundle == null)
            {
                Debug.LogError("Select asset bundle to load");
            }
            else
            {
                //Debug.Log(assetBundle.name);
                var myLoadedAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, $"belikovassets/{assetBundle.name}"));
                if (myLoadedAssetBundle == null)
                {
                    Debug.Log("Failed to load AssetBundle!");
                    return;
                }

                var assetNames = String.Join(", ", myLoadedAssetBundle.GetAllAssetNames());
                Debug.Log(assetNames);

                // here should be logic to replace all the usages of textures with new textures loaded from asset bundle

                myLoadedAssetBundle.Unload(true);
            }
        }
    }
}
