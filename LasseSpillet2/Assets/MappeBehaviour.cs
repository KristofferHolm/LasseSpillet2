using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MappeBehaviour : MonoBehaviour
{
    public Animator animator => _animator ?? (_animator = GetComponent<Animator>());
    public TextMeshProUGUI Score; 
    private Animator _animator;
    int Animations = 1;
    public List<AudioClip> mamfs;
    public AudioSource AudioSource => _AudioSource ?? (_AudioSource = GetComponent<AudioSource>());
    private AudioSource _AudioSource; 
    public void Animate()
    {
        animator.SetTrigger("Start");
        animator.SetInteger("Mail",Random.Range(0, Animations));

    }
    public void AnimationDone()
    {
        var points = CheckBox.Inst.PointsToBeCollected;
        Score.text = points.ToString();
        if (CheckBox.Inst.WasPerfectScore)
            GameManager.Inst.Lotte.SetTrigger("Win");
        GameManager.Inst.AddPointsToScore(points);
        PlaySound();
    }
    public void PlaySound()
    {
        var x = Random.Range(0, 2);
        AudioSource.PlayOneShot(mamfs[x]);
    }

}

