using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Others
{
    public class InputHandler : MonoBehaviour 
    {
        public event Action OnMove;
        public event Action<int> OnCast;
        public event Action<int> OnAttack;
    
        public bool CanMove = true;
        public bool CanCast = true;
        public bool CanAttack = true;

        private void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => CanMove)
                .Subscribe(_ => OnMove?.Invoke());
        
            Observable.EveryUpdate()
                .Where(_ => CanAttack && Input.GetMouseButtonDown(0))
                .Subscribe(_ => OnAttack?.Invoke(0));
            Observable.EveryUpdate()
                .Where(_ => CanAttack && Input.GetMouseButtonDown(1))
                .Subscribe(_ => OnAttack?.Invoke(1));
        
            this.UpdateAsObservable()
                .Where(_ => CanCast && Input.GetKeyDown(KeyCode.Alpha1))
                .Subscribe(_ => OnCast?.Invoke(0));
            this.UpdateAsObservable()
                .Where(_ => CanCast && Input.GetKeyDown(KeyCode.Alpha2))
                .Subscribe(_ => OnCast?.Invoke(1));
        }
    }
}
