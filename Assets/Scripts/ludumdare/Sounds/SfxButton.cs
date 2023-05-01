using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LudumDare.Sounds
{
    
    [RequireComponent(typeof(AudioSource))]
    public class SfxButton: MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
    {
        private AudioSource _audioSource;

        [SerializeField]
        public AudioClip onHover;
        [SerializeField]
        public AudioClip onClick;


        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            _audioSource.clip = onHover;
            _audioSource.Play();
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            _audioSource.clip = onClick;
            _audioSource.Play();
        }
    }
}