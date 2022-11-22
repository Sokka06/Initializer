using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Demo
{
    public class CountUI : MonoBehaviour
    {
        public TextMeshProUGUI CountText;
        
        private Player _player;

        private void Awake()
        {
            _player = FindObjectOfType<Player>();
        }

        private void Start()
        {
            OnPlayerDataChanged();
        }

        private void OnEnable()
        {
            _player.onDataChanged += OnPlayerDataChanged;
        }

        private void OnDisable()
        {
            _player.onDataChanged -= OnPlayerDataChanged;
        }

        private void OnPlayerDataChanged()
        {
            SetCountText(_player.Data.ConfettiLaunchCount);
        }

        private void SetCountText(int count)
        {
            CountText.SetText($"Count: {count}");
        }
    }
}