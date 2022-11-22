using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

namespace Demo
{
    public class ConfettiController : MonoBehaviour
    {
        public ParticleSystem[] Confetti;

        public event Action onLaunch;

        public void LaunchConfetti()
        {
            var confetti = Confetti[Random.Range(0, Confetti.Length)];
            confetti.Play();
            //confetti.Emit((int)confetti.emission.GetBurst(0).count.constant);
            onLaunch?.Invoke();
        }
    }

}
