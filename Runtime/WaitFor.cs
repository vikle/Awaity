using UnityEngine;

namespace Awaity
{
    public static class WaitFor
    {
        public static readonly YieldInstruction EndOfFrame = new WaitForEndOfFrame();
        public static readonly YieldInstruction FixedUpdate = new WaitForFixedUpdate();
    };
}
