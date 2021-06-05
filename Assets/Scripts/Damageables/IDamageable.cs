
using UnityEngine;

namespace KOS.Damageables
{
    public interface IDamageable
    {
        Vector3 position {get;}
        void TakeDamage(float amount, string origin);
    }
}
