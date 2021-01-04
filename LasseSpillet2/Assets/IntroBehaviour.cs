using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class IntroBehaviour : MonoBehaviour
{
    public AudioSource audio => _audio ?? (_audio = GetComponent<AudioSource>());
    private AudioSource _audio;
    public TextMeshProUGUI text => _text ?? (_text = GetComponentInChildren<TextMeshProUGUI>(true));
    private TextMeshProUGUI _text;

    public Image image => _image ?? (_image = GetComponent<Image>());
    private Image _image; 
    public void PlayIntro(string day)
    {
        GameManager.Inst.CurrentState = GameManager.GameState.Cutscene;
        image.enabled = true;
        text.gameObject.SetActive(true);
        text.text = day;
        audio.Play();
        StartCoroutine(WaitForSeconds(() =>
        {
            image.enabled = false;
            text.gameObject.SetActive(false);
            GameManager.Inst.CurrentState = GameManager.GameState.Idle;
        }));
    }
    IEnumerator WaitForSeconds(Action callback)
    {
        yield return new WaitForSeconds(audio.clip.length + 1.1f);
        callback.Invoke();
    }
}
