
using System.Collections.Generic;

using UnityEngine;

using KOS.Factories;

namespace KOS.Particles
{
    [CreateAssetMenu(menuName = "Factories/Particle Factory")]
    public class ParticleFactory : Factory
    {
        [SerializeReference]
        private List<ParticleGroup> particlesPrefabs;

        //> ADD A NEW REFERENCE TO FACTORY
        public void Add(ParticleGroup newParticles) => particlesPrefabs.Add(newParticles);

        //> GET AN INSTANCE OF ENEMY WITH INDEX
        public ParticleGroup Get(ParticleType index)
        {
            ParticleGroup instance = CreateInstance(particlesPrefabs[(int)index]);
            instance.originFactory = this;
            return instance;
        }
        
        //> RECLAIM AND DESTORY ENEMY
        public void Reclaim(ParticleGroup particles)
        {
            Debug.Assert(particles.originFactory == this, "Wrong Factory Reclaimation!");
            Destroy(particles.gameObject);
        }
    }
}

