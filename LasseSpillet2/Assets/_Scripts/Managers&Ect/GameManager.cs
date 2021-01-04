using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public Action<int> NewLevelInitiated;
    public int CurrentActiveToggles = 3;
    public Animator Lotte;
    public int Score;
    public TextMeshProUGUI ScoreText;
    int currentLevel = 0;
    public List<LevelDesign> Levels;
    public IntroBehaviour Intro;
    public GameObject NextLevelButton, FinalLevel;
    static float klokken8 = 480f;
    public clock Clock;
    public float TimeSinceLastLasseInterrupts = 0;
    public List<AudioClip> NotisClips;
    public Action Lasse;
    public Lasse LasseObj;
    float escape = 0;
    public AudioSource AudioSource => _AudioSource ?? (_AudioSource = GetComponent<AudioSource>());
    private AudioSource _AudioSource; 

    public enum GameState
    {
        Cutscene,
        OpenMail,
        WatchingMail,
        Idle,
        Lasse,
    }
    public GameState CurrentState = GameState.Cutscene;
    public bool NoMoreMails
    {
        get
        {
            return PC.Inst.Mails.Count == 0 && CurrentState == GameState.Idle;
        }
    }

    void Start()
    {
        InitiateNewLevel(currentLevel);
    }
    void Update()
    {
        TimeSinceLastLasseInterrupts += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.L))
            TryCallLasse(100,true);
        if (Input.GetKey(KeyCode.Escape))
        {
            escape += Time.deltaTime;
            if (escape > 1)
                Application.Quit();
        }
        else
            escape = 0;
        TryCallLasse(TimeSinceLastLasseInterrupts);
    }
    public void NextLevel()
    {
        currentLevel++;
        InitiateNewLevel(currentLevel);
    }
    void InitiateNewLevel(int level)
    {
        TimeSinceLastLasseInterrupts = 0;
        CurrentState = GameState.Idle;
        var ls = Levels[level];
        Intro.PlayIntro(ls.LevelName);
        PC.Inst.Mails = new List<MailCharacter>();
        StartCoroutine(MailsTicking(ls.Time, ls.Mails));
        CurrentActiveToggles = ls.ActiveToggles;
        CheckBox.Inst.UpdateCurrentActiveToggles(ls.ActiveToggles);
    }
    internal void AddPointsToScore(int points)
    {
        Score += points;
        ScoreText.text = "Total Score: \n" + Score.ToString();
        if (CurrentState == GameState.WatchingMail)
            CurrentState = GameState.Idle;
    }

    IEnumerator MailsTicking(float time, int mailCount)
    {
        float timePassed = 0;
        float timePassedSinceMailTicked = 0;
        float maxTime = (time / mailCount);
        int MailTickedIn = 0;
        while (timePassed < time && MailTickedIn < mailCount)
        {
            yield return null;
            Clock.UpdateClock(timePassed + klokken8);
            timePassed += Time.deltaTime * 2f;
           
            timePassedSinceMailTicked += NoMoreMails ? 5 * Time.deltaTime : Time.deltaTime;
           
            if (timePassedSinceMailTicked > maxTime)
            {
                TickMail();
                timePassedSinceMailTicked = 0;
                MailTickedIn++;
            }
            else if (timePassedSinceMailTicked % 4 > (4f-Time.deltaTime*6)  && Random.Range(0f, 100f) < timePassedSinceMailTicked)
            {
                TickMail();
                timePassedSinceMailTicked = 0;
                MailTickedIn++;
            }
        }       
        while (timePassed < time)
        {
            Clock.UpdateClock(timePassed + klokken8);
            timePassed += Time.deltaTime * 2f;
            if (NoMoreMails)
                break;
            yield return null;
        }
        while (CurrentState != GameState.Idle)
            yield return null;
        EndWave();
    }
    /// <summary>
    /// call every frame
    /// </summary>
    private void TryCallLasse(float timePassed,bool force = false)
    {
        if (timePassed < 10) return;
        //if (LasseInterruptsLeft <= 0) return;
        if (CurrentState == GameState.Lasse ||CurrentState == GameState.WatchingMail ||CurrentState == GameState.Cutscene) return;

        //checks every frame:
        var lottery = Random.Range(0f, 20f / Time.deltaTime);
        if (lottery > 1 && force == false) return;
        TimeSinceLastLasseInterrupts = 0;

        LasseObj.ComeIn();
        CurrentState = GameState.Lasse;
        Lasse?.Invoke();
    }

    public void EndWave()
    {
        CurrentState = GameState.Cutscene;
        if (currentLevel == Levels.Count - 1)
        {
            FinalLevel.SetActive(true);
            FinalLevel.GetComponentInChildren<finalscore>().FinalScore(Score);
        }
        else
            NextLevelButton.SetActive(true);
    }

    public void TickMail()
    {
        PC.Inst.AddMail();
        AudioSource.PlayOneShot(NotisClips[Random.Range(0, NotisClips.Count)]);
    }
  
}
[Serializable]
public class LevelDesign
{
    public string LevelName = "Mandag";
    public int ActiveToggles = 1;
    public int Mails;
    public int LasseInterupts = 0;
    public float Time = 8 * 60f; // sekunder
}

