using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using KOS.ItemData;

namespace KOS.Particles
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleGroup : MonoBehaviour //, IParticleData
    {
        //- PARTICLE FACTORY
        internal ParticleFactory originFactory;

        // //- OBJECT STATS
        // public ParticleData Data => enemy;
        // [SerializeField] protected ParticleData enemy = default;
        // public void SetData(Data data) => enemy = (ParticleData)data;

        //- POSITION HELPER
        public Vector3 position
        {
            get => transform.position;
            set => transform.position = value;
        }

        //- FORWARD HELPER
        public Vector3 forward
        {
            get => transform.forward;
            set => transform.forward = value;
        }

        //> INITIALIZE
        private ParticleSystem[] system;
        private void Awake() => system = GetComponentsInChildren<ParticleSystem>();

        //> SET SCALE OF ALL PARTICLES FOR UNIFORMITY
        public void SetScale(float scale)
        {
            foreach (var particles in system)
            {
                particles.transform.localScale = Vector3.one * scale;
            }
        }

        //> PLAY PARTICLE SEQUENCE
        public void Play() => system[0].Play();
    }
}
