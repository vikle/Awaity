using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Awaity
{
    public static class AwaiterEnumeratorPool
    {
        static readonly Stack<AwaiterEnumerator> sr_pool = new Stack<AwaiterEnumerator>(16);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AwaiterEnumerator Get()
        {
            var instance = (sr_pool.Count > 0) 
                ? sr_pool.Pop() 
                : new AwaiterEnumerator();
            
            return instance;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Release(AwaiterEnumerator instance)
        {
            sr_pool.Push(instance);
        }
    };
}
