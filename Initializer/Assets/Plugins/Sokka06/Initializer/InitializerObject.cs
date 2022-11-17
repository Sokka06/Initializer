using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sokka06.Initializer
{
    public abstract class InitializerObject : ScriptableObject
    {
        [Tooltip("Execute Initializer on Play.")]
        public bool Active = true;
        
        protected const string MENU_NAME = "Initializer/";

        public abstract void Initialize();
    }
}
