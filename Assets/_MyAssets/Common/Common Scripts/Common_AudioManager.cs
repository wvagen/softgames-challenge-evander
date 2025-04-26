using UnityEngine.Audio;
using UnityEngine;
using System;
using Unity.VisualScripting;

namespace Softgames.Common
{
    public class Common_AudioManager : MonoBehaviour
    {
        [SerializeField] private CustomSounds[] _sounds;

        public static Common_AudioManager audioManInstance;

        private void Awake()
        {
            if (audioManInstance == null)
            {
                audioManInstance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);

            foreach (CustomSounds s in _sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }

        private void Start()
        {
            Play_Music("Music");
        }

        public void Play_Sfx(string name)
        {
            CustomSounds s = Array.Find(_sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound " + name + " not Found!");
            }
            s.source.Play();
        }

        public void Stop_Sfx(string name)
        {
            CustomSounds s = Array.Find(_sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound " + name + " not Found!");
            }
            s.source.Stop();
        }

        public void Mute_Sfx(bool m)
        {
            foreach (CustomSounds s in _sounds)
            {
                if (s.name != "Music")
                {
                    s.source.mute = m;
                }
            }
        }

        private void Play_Music(string name)
        {
            CustomSounds s = Array.Find(_sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.LogWarning("Sound " + name + " not Found!");
            }
            s.source.Play();
        }

        public void Mute_Music(bool m)
        {
            foreach (CustomSounds s in _sounds)
            {
                if (s.name == "Music")
                {
                    s.source.mute = m;
                    break;
                }
            }
        }
    }

    [Serializable]
    public class CustomSounds
    {
        public string name;

        public AudioClip clip;

        [Range(0f, 1f)]
        public float volume;

        [Range(0.1f, 1f)]
        public float pitch;

        public bool loop;

        [HideInInspector]
        public AudioSource source;
    }

    [Serializable]
    public struct Toggles
    {
        public GameObject onSwitch;
        public GameObject offSwitch;
    }
}