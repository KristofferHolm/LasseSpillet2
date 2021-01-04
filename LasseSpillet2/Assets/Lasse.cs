using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Lasse : MonoBehaviour
{
    public Animator animator => _animator ?? (_animator = GetComponent<Animator>());
    private Animator _animator;
    int WhichItem = -1;
    public AudioSource audioSource => _audioSource ?? (_audioSource = GetComponent<AudioSource>());
    private AudioSource _audioSource;
    public List<AudioClip> AudioClips;
    internal void ComeIn()
    {
        animator.SetTrigger("Ind");
        WhichItem = Random.Range(0, 5);
        audioSource.PlayOneShot(AudioClips[WhichItem]);
    }
    public void ItemFind(int i)
    {
        if (i != WhichItem) return;
        animator.SetTrigger("Ud");
        audioSource.PlayOneShot(AudioClips[5]);
        WhichItem = -1;
        GameManager.Inst.TimeSinceLastLasseInterrupts = 0;
        GameManager.Inst.CurrentState = GameManager.GameState.Idle;
    }
}
