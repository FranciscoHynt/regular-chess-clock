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
        private static PooledObject currentLoopSound;
        private static readonly GameSettings Settings;

        private const string MASTER = "Master";

        static InGameSoundManager()
        {
            Settings = MainAssets.I.gameSettings;
        }

        public static void PlaySound(SingleSound singleSound, float volume)
        {
            if (!Settings.shouldPlaySounds)
                return;

            PooledObject soundGameObject = MainAssets.I.soundObject.GetPooledInstance<PooledObject>();

            AudioSource audioSource = soundGameObject.GetComponent<AudioSource>();
            audioSource.clip = GetAudioClip(singleSound);
            audioSource.maxDistance = 100f;
            audioSource.spatialBlend = 0.5f;
            audioSource.volume = volume;
            audioSource.loop = false;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.outputAudioMixerGroup = MainAssets.I.soundMixer.FindMatchingGroups(MASTER)[0];

            audioSource.Play();

            soundGameObject.CallWithDelay(() => soundGameObject.ReturnToPool(), audioSource.clip.length);
        }

        public static void StopLoopSound()
        {
            if(currentLoopSound)
                currentLoopSound.ReturnToPool();
        }

        public static void PlayLoopSound(LoopSound singleSound, float volume)
        {
            if (!Settings.shouldPlaySounds)
                return;

            PooledObject soundGameObject = MainAssets.I.soundObject.GetPooledInstance<PooledObject>();
            currentLoopSound = soundGameObject;
            
            AudioSource audioSource = soundGameObject.GetComponent<AudioSource>();
            audioSource.clip = GetAudioClip(singleSound);
            audioSource.maxDistance = 100f;
            audioSource.spatialBlend = 0.5f;
            audioSource.volume = volume;
            audioSource.loop = true;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.dopplerLevel = 0f;
            audioSource.outputAudioMixerGroup = MainAssets.I.soundMixer.FindMatchingGroups(MASTER)[0];

            audioSource.Play();
        }

        private static AudioClip GetAudioClip(SingleSound singleSound)
        {
            return (MainAssets.I.soundClipsList
                    .Where(soundAudioClip => soundAudioClip.singleSound == singleSound)
                    .Select(soundAudioClip => soundAudioClip.audioClip))
                .FirstOrDefault();
        }

        private static AudioClip GetAudioClip(LoopSound loopSound)
        {
            return (MainAssets.I.loopSoundAudioClips
                    .Where(soundAudioClip => soundAudioClip.loopSound == loopSound)
                    .Select(soundAudioClip => soundAudioClip.audioClip))
                .FirstOrDefault();
        }
    }
}