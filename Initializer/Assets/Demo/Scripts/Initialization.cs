using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Demo
{
    public class Initialization : MonoBehaviour
    {
        public AssetReference MainMenu;
        
        private IEnumerator Start()
        {
            // Dont destroy while running initialization.
            DontDestroyOnLoad(gameObject);
        
            // Log player in
            var loginWait = new WaitForSeconds(3f);
            yield return loginWait;
        
            // Load Save data
            DataManager.Instance.Load<PlayerData>();
        
            // Load Main Menu
            yield return MainMenu.LoadSceneAsync();

            // Initialization done, destroy
            Destroy(gameObject);
        }
    }
}