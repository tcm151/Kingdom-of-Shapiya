using UnityEngine;

namespace KOS
{
    static public class UnityExtensions
    {
        static public bool Contains(this LayerMask layerMask, int layer) => (layerMask == (layerMask | (1 << layer)));
    }
}
