using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class finalscore : MonoBehaviour
{
    public TextMeshProUGUI text => _text ?? (_text = GetComponent<TextMeshProUGUI>());
    private TextMeshProUGUI _text; 
    public void FinalScore(int score)
    {
        text.text = "Din Score blev: " + score.ToString();
    }
}
