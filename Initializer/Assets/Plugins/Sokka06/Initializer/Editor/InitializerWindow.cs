using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Sokka06.Initializer
{
    public class InitializerWindow : EditorWindow
    {
        private InitializerObject _initializerObject;

        private const string NAME = "Initializer";
        
        [MenuItem("Tools/" + NAME + "/Settings")]
        public static void ShowWindow()
        {
            EditorWindow window = GetWindow<InitializerWindow>(NAME + " Settings");
            window.minSize = new Vector2(200, 100);
        }

        private void OnEnable()
        {
            _initializerObject = Initializer.LoadInitializer();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField(NAME + " Settings", EditorStyles.boldLabel);

            if (_initializerObject != null)
            {
                var serializedObject = new SerializedObject(_initializerObject);
                var editor = Editor.CreateEditor(_initializerObject);
   
                EditorGUILayout.BeginVertical("HelpBox");
                editor.DrawDefaultInspector();
                EditorGUILayout.EndVertical();
                
                serializedObject.ApplyModifiedProperties();
            }
            else
            {
                EditorGUILayout.HelpBox(Initializer.LOAD_ERROR_MESSAGE, MessageType.Info);
            }
        }
    }
}