using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KOS.Factories;

namespace KOS.Projectiles
{
    [CreateAssetMenu(menuName = "Factories/Projectile Factory")]
    public class ProjectileFactory : Factory
    {
        [SerializeReference] private List<Projectile> projectilePrefabs;

        //> ADD A NEW REFERENCE TO FACTORY
        public void Add(Projectile newProjectile) => projectilePrefabs.Add(newProjectile);

        //> GET INSTANCE OF PROJECTILE BY TYPE
        public Projectile Get(ProjectileType index)
        {
            Projectile projectile = CreateInstance(projectilePrefabs[(int)index]);
            projectile.originFactory = this;
            return projectile;
        }

        //> GET MANY INSTANCES OF PROJECTILE BY TYPE
        public Projectile[] Get(ProjectileType index, int amount)
        {
            Projectile[] projectiles = new Projectile[amount];
            for (int i = 0; i < projectiles.Length; i++)
            {
                projectiles[i] = CreateInstance(projectilePrefabs[(int)index]);
                projectiles[i].originFactory = this;
            }
            return projectiles;
        }

        //> RECLAIM AND DESTROY PROJECTILE
        public void Reclaim(Projectile projectile)
        {
            Debug.Assert(projectile.originFactory == this, "Wrong Factory Reclamation!");
            Destroy(projectile.gameObject);
        }

    }
}
