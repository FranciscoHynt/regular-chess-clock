using System;
using System.Collections.Generic;
using Enumerators;
using ObjectPool;
using Settings;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets
{
    public class MainAssets : MonoBehaviour
    {
        private static MainAssets _i;

        public static MainAssets I
        {
            get
            {
                if (_i == null)
                    _i = Resources.Load<MainAssets>("MainAssets");

                return _i;
            }
        }

        [Header("Settings")] 
        public GameSettings gameSettings;
        public SoundSettings soundSettings;

        [Header("Object")] 
        public PooledObject soundObject;

        [Header("Sounds")] 
        public List<SoundAudioClip> soundClipsList;
        public List<LoopSoundAudioClip> loopSoundAudioClips;

        [Header("Mixer")] 
        public AudioMixer soundMixer;


        [Serializable]
        public class SoundAudioClip
        {
            public SingleSound singleSound;
            public AudioClip audioClip;
        }

        [Serializable]
        public class LoopSoundAudioClip
        {
            public LoopSound loopSound;
            public AudioClip audioClip;
        }
    }
}