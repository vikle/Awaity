using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Awaity
{
    public sealed class AwaityCoroutineRunner : MonoBehaviour
    {
        static AwaityCoroutineRunner s_instance;
        static bool s_initialized;

        void OnDestroy()
        {
            s_instance = null;
            s_initialized = false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void StartExternalCoroutine(IEnumerator routine)
        {
            if (!s_initialized)
            {
                s_initialized = true;
                CreateGameObject();
            }

            s_instance.StartCoroutine(routine);
        }

        private static void CreateGameObject()
        {
            var g_obj = new GameObject(nameof(AwaityCoroutineRunner)) { isStatic = true };
            s_instance = g_obj.AddComponent<AwaityCoroutineRunner>();
            DontDestroyOnLoad(g_obj);
        }
    };
}
