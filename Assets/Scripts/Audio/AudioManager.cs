
using System;
using UnityEngine;

namespace KOS.Audio
{
    public class AudioManager : MonoBehaviour
    {
        //- SINGLETON
        static private AudioManager instance;
        static public AudioManager Active
        {
            get
            {
                if (!instance) Debug.LogError("No <color=red>AudioManager</color> in scene!");
                return instance;
            }
        }

        //- LOCAL VARIABLES
        [SerializeField] public Sound[] sounds;
        // private int audioStreams = 4;
        [SerializeField] private AudioSource[] sources;

        //> INITIALIZATION
        private void Awake() 
        {
            instance = this;

            // for (int i = 0; i < audioStreams; i++) gameObject.AddComponent<AudioSource>();
            sources = GetComponents<AudioSource>();
        }

        //> PLAY ONE SHOT SOUND AT POINT IN WORLD
        public void PlayAtPoint(string soundName, Vector3 point)
        {
            Sound s = Array.Find(sounds, sound => sound.name == soundName);
            if (s != null) AudioSource.PlayClipAtPoint(s.clip, point, s.volume);
            else Debug.LogWarning($"Unable to find sound: <color=yellow>{soundName}</color>");
        }

        //> PLAY ONE SHOT SOUND CLIP
        public void PlayOneShot(string soundName)
        {
            Sound s = Array.Find(sounds, sound => sound.name == soundName);
            if (s != null) sources[0].PlayOneShot(s.clip, s.volume);
            else Debug.LogWarning($"Unable to find sound: <color=yellow>{soundName}</color>");
        }

        //> REPLACE STREAM SOUND CLIP
        public void Play(string soundName, int stream = 0)
        {
            Sound s = Array.Find(sounds, sound => sound.name == soundName);
            if (s is null)
            {
                Debug.LogWarning($"Unable to find sound: <color=yellow>{soundName}</color>");
                return;
            }
            sources[stream].clip = s.clip;
            sources[stream].pitch = s.pitch;
            sources[stream].volume = s.volume;
            sources[stream].Play();
        }

        //> STOP STREAM SOUND CLIP
        public void Stop(int stream)
        {
            Debug.Log("Stopping Sound: " + sources[stream].clip.name);
            sources[stream].Stop();
        }
    }
}

