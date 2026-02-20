using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Awaity
{
    public static class AwaiterExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AwaityAwaiter GetAwaiter(this IEnumerator coroutine)
        {
            var controller = AwaiterEnumeratorPool.Get();
            return new AwaityAwaiter(controller, coroutine);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AwaityAwaiter GetAwaiter(this YieldInstruction instruction)
        {
            var controller = AwaiterEnumeratorPool.Get();
            return new AwaityAwaiter(controller, instruction);
        }
    };
}
