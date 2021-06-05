using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOS.Explodeables
{
    public interface IExplodeable
    {
        void Explode(Vector3 point);
    }
}
