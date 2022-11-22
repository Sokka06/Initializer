using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Demo
{
    [Serializable]
    public class PlayerData : IData
    {
        public int ConfettiLaunchCount { get; set; }
    }
    
    public class Player : MonoBehaviour
    {
        public ConfettiController ConfettiController;
        
        private DataManager _dataManager;
        
        public PlayerData Data { get; private set; }
        public event Action onDataChanged;

        private void OnEnable()
        {
            _dataManager = DataManager.Instance;
            Data = DataManager.Instance.Get<PlayerData>();
            
            ConfettiController.onLaunch += OnConfettiLaunch;
        }

        private void OnDisable()
        {
            ConfettiController.onLaunch -= OnConfettiLaunch;
            _dataManager.Save(Data);
        }
        
        // Update is called once per frame
        void Update()
        {
            var mouse = Mouse.current;
            var touch = Touchscreen.current;

            if (mouse != null)
            {
                if (mouse.leftButton.wasPressedThisFrame)
                {
                    ConfettiController.LaunchConfetti();
                }
            }
            
            if (touch != null)
            {
                if (touch.press.wasPressedThisFrame)
                {
                    ConfettiController.LaunchConfetti();
                }
            }
        }
        
        private void OnConfettiLaunch()
        {
            Data.ConfettiLaunchCount++;
            onDataChanged?.Invoke();
        }
    }
}