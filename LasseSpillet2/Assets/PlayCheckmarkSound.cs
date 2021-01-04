using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayCheckmarkSound : MonoBehaviour
{
    public AudioSource audioSource => _audioSource ?? (_audioSource = GetComponent<AudioSource>());
    private AudioSource _audioSource;
    public List<AudioClip> AudioClips;
   public void PlaySound()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(AudioClips[Random.Range(0, AudioClips.Count)]);
    }
}
