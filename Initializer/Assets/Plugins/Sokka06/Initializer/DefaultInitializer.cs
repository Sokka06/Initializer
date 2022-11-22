using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Sokka06.Initializer
{
    /// <summary>
    /// Loads an Addressable Scene on initialize.
    /// </summary>
    [CreateAssetMenu(fileName = Initializer.ADDRESSABLE_NAME, menuName = MENU_NAME + "Default Initializer")]
    public class DefaultInitializer : InitializerObject
    {
        [Header("Default")]
        public AssetReference Scene;

        public override void Initialize()
        {
            if (Active)
            {
#if UNITY_EDITOR
                //Avoid opening same scene twice if already open in editor.
                for (int i = 0; i < UnityEditor.SceneManagement.EditorSceneManager.sceneCount; i++)
                {
                    var scene = UnityEditor.SceneManagement.EditorSceneManager.GetSceneAt(i);
                    var g0 = UnityEditor.AssetDatabase.AssetPathToGUID(scene.path);
                    var g1 = Scene.AssetGUID;
                    
                    // Debug.Log($"Current: {g0}, Loaded: {g1}");
                    if (g0 == g1)
                    {
                        // Debug.Log("Already loaded, skipping...");
                        return;
                    }
                }
#endif
                var op = Scene.LoadSceneAsync();
                var result = op.WaitForCompletion();
                result.ActivateAsync();
            }
        }
    }
}