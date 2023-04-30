using UnityEngine;
using UnityEngine.Audio;

namespace LudumDare.Settings
{
    public class AudioManager: MonoBehaviour
    {
        [SerializeField]
        private AudioMixer audioMixer;
        
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
    }
}