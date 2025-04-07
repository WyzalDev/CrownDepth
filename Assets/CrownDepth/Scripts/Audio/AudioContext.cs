using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using WyzalUtilities.Data;

namespace WyzalUtilities.Audio
{
    public class AudioContext : MonoBehaviour
    {
        #region Inspector Fields

        [SerializeField] private string defaultContainer;
        [SerializeField] private SerializableDictionary<string, AudioContainer> audioContainers;
        [SerializeField] private AudioSource globalMusicAudioSource;
        [SerializeField] private AudioSource globalSfxAudioSource;

        #endregion

        #region Properties

        public static AudioContext Instance { get; private set; }
        public string DefaultContainerName => defaultContainer;
        public const float fadeDuration = 2f;

        #endregion

        #region Initialization

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Play music
        /// </summary>
        /// <param name="musicName">Music name</param>
        /// <param name="fadeSettings">Class that handles fade parameters. if null - no fade applied</param>
        /// <param name="containerName">Container name where music stores. if null - default container will be used</param>
        /// <param name="audioSource">Audio Source to play music. if null - global audio source will be used.</param>
        public static Sequence PlayGlobalMusic(string musicName, FadeSettings fadeSettings = null,
            string containerName = null, AudioSource audioSource = null)
        {
            if (fadeSettings == null)
            {
                Instance.PlayMusic(musicName, containerName, audioSource);
                return null;
            }

            return Instance.PlayMusicWithFade(musicName, fadeSettings, containerName, audioSource);
        }

        /// <summary>
        /// Play sfx
        /// </summary>
        /// <param name="sfxName">Sfx name</param>
        /// <param name="containerName">Container name where sfx stores. if null - default container will be used</param>
        /// <param name="audioSource">Audio Source to play sfx. if null - global audio source will be used.</param>
        public static void PlayGlobalSfx(string sfxName, string containerName = null, AudioSource audioSource = null)
        {
            Instance.PlayOneShot(sfxName, containerName, audioSource);
        }

        public static float GetSoundDuration(string soundName, SoundType soundType, string containerName = null)
        {
            return Instance.FindDurationOfSound(soundName, soundType, containerName);
        }

        public static AudioSource GetSoundSource(SoundType soundType)
        {
            return Instance.GetSource(soundType);
        }

        public static IEnumerator TurnOffGlobalSounds(float duration = fadeDuration)
        {
            yield return Instance.TurnOffSoundSource(duration);
        }
        
        public static IEnumerator TurnOffGlobalMusic(float duration = fadeDuration)
        {
            yield return Instance.TurnOffMusicSource(duration);
        }

        public static IEnumerator TurnOnSource(float duration = fadeDuration)
        {
            yield return Instance.TurnOnGlobalSource(duration);
        }

        public static void ClearMusic()
        {
            Instance.ClearGlobalMusic();
        }
        #endregion

        #region Private Methods
        private void ClearGlobalMusic()
        {
            globalMusicAudioSource.clip = null;
        }
        private IEnumerator TurnOnGlobalSource(float duration, Ease easeType = Ease.InOutQuart)
        {
            var sequence = DOTween.Sequence();
            yield return sequence.Append(globalMusicAudioSource.DOFade(1f, duration).SetEase(easeType))
                .Join(globalSfxAudioSource.DOFade(1f, duration).SetEase(easeType))
                .WaitForCompletion();
        }
        
        private IEnumerator TurnOffMusicSource(float duration, Ease easeType = Ease.InOutQuart)
        {
            var sequence = DOTween.Sequence();
            yield return sequence.Append(globalMusicAudioSource.DOFade(0f, duration).SetEase(easeType))
                .WaitForCompletion();
        }

        private IEnumerator TurnOffSoundSource(float duration, Ease easeType = Ease.InOutQuart)
        {
            var sequence = DOTween.Sequence();
            yield return sequence.Append(globalMusicAudioSource.DOFade(0f, duration).SetEase(easeType))
                .Join(globalSfxAudioSource.DOFade(0f, duration).SetEase(easeType))
                .WaitForCompletion();
        }
        
        private AudioSource GetSource(SoundType soundType)
        {
            switch (soundType)
            {
                case SoundType.Music:
                    return globalMusicAudioSource;
                default:
                    return globalSfxAudioSource;
            }
        }

        private Sequence PlayMusicWithFade(string musicName, FadeSettings fadeSettings, string containerName = null,
            AudioSource audioSource = null)
        {
            if (audioSource == null)
                audioSource = globalMusicAudioSource;
            var endVolume = FindVolumeOfSound(musicName, SoundType.Music, containerName);

            var sequence = DOTween.Sequence();
            sequence.Append(
                audioSource.DOFade(0, fadeSettings.durationIn).SetEase(fadeSettings.easeIn).SetUpdate(true)
                    .OnComplete(() => PlayMusic(musicName, containerName, audioSource)));
            sequence.Append(
                audioSource.DOFade(endVolume, fadeSettings.durationOut).SetEase(fadeSettings.easeOut).SetUpdate(true));
            return sequence;
        }

        private void PlayMusic(string musicName, string containerName = null,
            AudioSource audioSource = null)
        {
            //check for globals audioSource
            if (audioSource == null)
                audioSource = globalMusicAudioSource;

            containerName ??= defaultContainer;

            var audioContainer = audioContainers[containerName];

            if (audioContainer == null)
            {
                Debug.LogException(new Exception($"Can't find audio container {containerName} in audio context."));
                return;
            }

            //play sound
            if (audioContainer.TryFindSound(out var musicSound, musicName, SoundType.Music))
            {
                audioSource.clip = musicSound.clip;
                audioSource.volume = musicSound.volume;
                audioSource.loop = true;
                audioSource.Play();
            }
            else
            {
                Debug.LogException(new Exception($"Can't find Sound in audio container {containerName}."));
            }
        }

        private float FindVolumeOfSound(string soundName, SoundType soundType, string containerName = null)
        {
            containerName ??= defaultContainer;
            var audioContainer = audioContainers[containerName];

            if (audioContainer.TryFindSound(out var sound, soundName, soundType))
            {
                return sound.volume;
            }
            else
            {
                Debug.LogException(new Exception($"Can't find Sound in audio container {containerName}."));
                return 0;
            }
        }

        private float FindDurationOfSound(string soundName, SoundType soundType, string containerName = null)
        {
            containerName ??= defaultContainer;
            var audioContainer = audioContainers[containerName];

            if (audioContainer.TryFindSound(out var sound, soundName, soundType))
            {
                return sound.clip.length;
            }
            else
            {
                Debug.LogException(new Exception($"Can't find Sound in audio container {containerName}."));
                return 0;
            }
        }

        private void PlayOneShot(string sfxName, string containerName = null,
            AudioSource audioSource = null)
        {
            //check for globals audioSource
            if (audioSource == null)
                audioSource = globalSfxAudioSource;

            containerName ??= defaultContainer;

            var audioContainer = audioContainers[containerName];

            if (audioContainer == null)
            {
                Debug.LogException(new Exception($"Can't find audio container {containerName} in audio context."));
                return;
            }

            //play sound
            if (audioContainer.TryFindSound(out var sfxSound, sfxName, SoundType.Sfx))
            {
                audioSource.PlayOneShot(sfxSound.clip, sfxSound.volume);
            }
            else
            {
                Debug.LogException(new Exception($"Can't find Sound in audio container {containerName}."));
            }
        }

        #endregion
    }

    public class FadeSettings
    {
        public float durationIn = 0.5f;
        public Ease easeIn;
        public float durationOut = 0.5f;
        public Ease easeOut;
    }
}