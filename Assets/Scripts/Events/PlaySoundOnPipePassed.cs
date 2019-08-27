//Version dechut, voir scoreUpSound class

/*using System;
using UnityEngine;

namespace Game
{
    public class PlaySoundOnPipePassed : MonoBehaviour
    {
        private ScoreEventChannel scoreEventChannel;
        private AudioSource audioSource;
        private void Awake()
        {
            scoreEventChannel = Finder.ScoreEventChannel;

            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            scoreEventChannel.OnScored += PlaySound;
        }

        private void OnDisable()
        {
            scoreEventChannel.OnScored += PlaySound;
        }

        private void PlaySound()
        {
            Debug.Log("yes");
            audioSource.Play();
        }
    }
}*/