using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class NextDayButton : MonoBehaviour
{
    public AudioSource audio => _audio ?? (_audio = GetComponent<AudioSource>());
    private AudioSource _audio;
    public Button Button => _Button ?? (_Button = GetComponent<Button>());
    private Button _Button; 
    void OnEnable()
    {
        audio.Play();
        gameObject.GetComponent<CanvasGroup>().DOFade(1, 2.5f).OnComplete(() => Button.interactable = true);
    }
    void OnDisable()
    {
        Button.interactable = false;
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }

}
