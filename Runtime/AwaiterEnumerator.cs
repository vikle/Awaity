using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Awaity
{
    public sealed class AwaiterEnumerator : IEnumerator
    {
        Action m_continuation;
        IEnumerator m_coroutine;
        YieldInstruction m_instruction;
        
        public bool IsCompleted
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]get;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]private set;
        }
        
        public object Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]get;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]private set;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Init(object routine)
        {
            m_coroutine = (routine as IEnumerator);
            m_instruction = (routine as YieldInstruction);
            AwaityCoroutineRunner.StartExternalCoroutine(this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void OnCompleted(Action continuation)
        {
            m_continuation = continuation;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            if (m_coroutine != null)
            {
                bool move_next = m_coroutine.MoveNext();
                Current = m_coroutine.Current;
                if (!move_next) Complete();
                return move_next;
            }

            if (Current == null)
            {
                Current = m_instruction;
                return true;
            }

            Complete();
            return false;
        }

        public void Reset()
        {
            Complete();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Complete()
        {
            Current = null;
            IsCompleted = false;
            
            m_continuation?.Invoke();
            m_continuation = null;
            
            AwaiterEnumeratorPool.Release(this);
        }
    };
}
