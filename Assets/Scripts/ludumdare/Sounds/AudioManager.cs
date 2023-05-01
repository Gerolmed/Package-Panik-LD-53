using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace LudumDare.Sounds
{
    public class AudioManager: MonoBehaviour
    {
        [SerializeField]
        private AudioMixer audioMixer;
        [SerializeField]
        private Slider musicSlider;
        [SerializeField]
        private Slider sfxSlider;
        
        public void SetMusicVolume(float volume)
        {
            var actualValue = Mathf.Clamp(Mathf.Log10(volume) * 20, -80f, 0f);
            audioMixer.SetFloat("MusicVolume", actualValue);
        }
        
        public void SetSfxVolume(float volume)
        {
            var actualValue = Mathf.Clamp(Mathf.Log10(volume) * 20, -80f, 0f);

            audioMixer.SetFloat("SFXVolume", actualValue);
        }


        private void Awake()
        {
            audioMixer.GetFloat("MusicVolume", out var musicVolume);
            audioMixer.GetFloat("SFXVolume", out var sfxVolume);
            var musicValue = Mathf.Pow(10, musicVolume / 20);
            var sfxValue = Mathf.Pow(10, sfxVolume / 20);
            
            musicSlider.value = musicValue;
            sfxSlider.value = sfxValue;
        }
    }
}