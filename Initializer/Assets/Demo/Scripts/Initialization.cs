using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Demo
{
    /// <summary>
    /// Very very basic game initializer.
    /// </summary>
    public class Initialization : MonoBehaviour
    {
        public AssetReference Game;
        
        private IEnumerator Start()
        {
            // Dont destroy while running initialization.
            DontDestroyOnLoad(gameObject);
            
            // Restore settings
#if UNITY_ANDROID
            if (!Application.isEditor)
            {
                Application.targetFrameRate = 60;
                QualitySettings.vSyncCount = 0;
                Screen.sleepTimeout = SleepTimeout.NeverSleep;
            }
#endif
        
            // Log player in
            var loginWait = new WaitForSeconds(3f);
            yield return loginWait;
        
            // Load Save data
            DataManager.Instance.Load<PlayerData>();
        
            // Load Game scene
            yield return Game.LoadSceneAsync();

            // Initialization done, destroy
            Destroy(gameObject);
        }
    }
}