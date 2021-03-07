using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Test
{
    public class TestPlayerMovement : MonoBehaviour
    {
        private Animator _animator;

        private int _isRunnintHash;

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            _isRunnintHash = Animator.StringToHash("isRunning");
        }

        private void Start()
        {
            // this.UpdateAsObservable()
            //     .Where().
        }

        private void HandleMovement()
        {
            bool isRunning = _animator.GetBool(_isRunnintHash);
        }
    }
}
