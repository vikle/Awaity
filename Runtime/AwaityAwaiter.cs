using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Awaity
{
    public readonly struct AwaityAwaiter : ICriticalNotifyCompletion
    {
        readonly AwaiterEnumerator m_controller;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private AwaityAwaiter(AwaiterEnumerator controller)
        {
            m_controller = controller;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AwaityAwaiter(AwaiterEnumerator controller, IEnumerator enumerator) : this(controller)
        {
            controller.Init(enumerator);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public AwaityAwaiter(AwaiterEnumerator controller, YieldInstruction instruction) : this(controller)
        {
            controller.Init(instruction);
        }
        
        public bool IsCompleted
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => m_controller.IsCompleted;
        }
        
        public void OnCompleted(Action continuation)
        {
            m_controller.OnCompleted(continuation);
        }

        public void UnsafeOnCompleted(Action continuation)
        {
            m_controller.OnCompleted(continuation);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object GetResult()
        {
            return m_controller.Current;
        }
    };
}
