using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Sokka06.Initializer
{
    /*
     // Not used for now
     public enum InitializerMode
    {
        None,
        Editor,
        Player,
        All
    }*/
    
    public static class Initializer
    {
        public const string ADDRESSABLE_NAME = "Initializer";
        public static readonly string LOAD_ERROR_MESSAGE = $"No Initializer Asset with name '{ADDRESSABLE_NAME}' found! Make sure your Initializer Asset inherits from InitializerObject and is marked as Addressable and uses '{ADDRESSABLE_NAME}' as its addressable name.";

        /// <summary>
        /// Loads Initializer Addressable.
        /// </summary>
        /// <returns></returns>
        public static InitializerObject LoadInitializer()
        {
            var op = Addressables.LoadAssetAsync<InitializerObject>(ADDRESSABLE_NAME);
            op.WaitForCompletion();
            
            switch (op.Status)
            {
                case AsyncOperationStatus.None:
                    break;
                case AsyncOperationStatus.Succeeded:
                    return op.Result;
                case AsyncOperationStatus.Failed:
                    Debug.LogWarning(LOAD_ERROR_MESSAGE);
                    break;
                default:
                    return null;
            }

            return null;
        }
        
        /// <summary>
        /// <para>Executed when entering Play mode or starting application.</para>
        /// <para>NOTE: Awake and OnEnable will still run in Editor before this is executed and as far as I know there's currently no way around this.</para>
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            var initializer = LoadInitializer();

            if (initializer != null)
                initializer.Initialize();
        }
    }
}