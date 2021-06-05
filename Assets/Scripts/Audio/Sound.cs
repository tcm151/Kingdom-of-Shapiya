
using System;
using UnityEngine;

namespace KOS.Audio
{
    [Serializable]
    public class Sound
    {
        public string name; 
        public AudioClip clip;
        public float pitch;
        public float volume;
    }
}