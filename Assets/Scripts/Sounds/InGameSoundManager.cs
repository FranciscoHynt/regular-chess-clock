using System.Linq;
using Assets;
using Enumerators;
using ObjectPool;
using Settings;
using UnityEngine;
using Utils;

namespace Sounds
{
    public static class InGameSoundManager
    {
        private static readonly GameSettings Settings;

        private const string MASTER = "Master";

        static InGameSoundManager()
        {
            Settings = MainAssets.I.gameSettings;
        }

        public static void PlaySound(Sound sound, float volume)
        {
            if (!Settings.shouldPlaySounds)
                return;

            PooledObject soundGameObject = MainAssets.I.soundObject.GetPooledInstance<PooledObject>();

            AudioSource audioSource = soundGameObject.GetComponent<AudioSource>();
            audioSource.clip = GetAudioClip(sound);
            audioSource.maxDistance = 100f;
            audioSource.spatialBlend = 0.5f;
            audioSource.volume = volume;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.outputAudioMixerGroup = MainAssets.I.soundMixer.FindMatchingGroups(MASTER)[0];

            audioSource.Play();

            soundGameObject.CallWithDelay(() => soundGameObject.ReturnToPool(), audioSource.clip.length);
        }

        private static AudioClip GetAudioClip(Sound sound)
        {
            return (MainAssets.I.soundClipsList
                    .Where(soundAudioClip => soundAudioClip.sound == sound)
                    .Select(soundAudioClip => soundAudioClip.audioClip))
                .FirstOrDefault();
        }
    }
}