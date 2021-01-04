using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBox : Singleton<CheckBox>
{
    public OpenMail OpenMail;
    public List<Toggle> Toggles;
    int activeToggles = 0;
    public int PointsToBeCollected;
    public bool WasPerfectScore;

    void Awake()
    {
        GameManager.Inst.NewLevelInitiated += UpdateCurrentActiveToggles;
    }

    public  void UpdateCurrentActiveToggles(int level)
    {
        for (int i = 0; i < Toggles.Count; i++)
        {
            Toggles[i].gameObject.SetActive(i< level);
        }
    }

    /// <summary>
    /// ButtonPress
    /// </summary>
    public void FinnishToggle()
    {
        var fits = OpenMail.CurrentMailCharacter.FitsInCheckbox;
        var numberOfCorrectAnswers = 0;
        WasPerfectScore = true;
        for (int i = 0; i < GameManager.Inst.CurrentActiveToggles; i++)
        {
            if (Toggles[i].isOn == fits[i])
                numberOfCorrectAnswers++;
            else
                WasPerfectScore = false;
        }
        PointsToBeCollected = numberOfCorrectAnswers * 100;

        PC.Inst.CloseMail();
        GameManager.Inst.CurrentState = GameManager.GameState.WatchingMail;
    }

    internal void UnToggleAll()
    {
        foreach (var item in Toggles)
        {
            item.isOn = false;
        }
    }
}
