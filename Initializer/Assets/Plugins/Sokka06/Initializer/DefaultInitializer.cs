using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

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
            if(Active)
                Addressables.LoadSceneAsync(Scene);
        }
    }
}