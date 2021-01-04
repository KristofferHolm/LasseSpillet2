using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using DG.Tweening;
public class PC : Singleton<PC>
{
    [ShowInInspector]
    public List<MailCharacter> Mails;

    public OpenMail OpenMailWindow;

    public MappeBehaviour Mappe;

    public GameObject Notification;
    public TextMeshProUGUI NumberOfMails;


    void Start()
    {
        Mails = new List<MailCharacter>();
        UpdateVisual(true);
    }
    /// <summary>
    /// Button Press Action
    /// </summary>
    public void OpenMail()
    {
        if (Mails.Count == 0) return;
        if (GameManager.Inst.CurrentState != GameManager.GameState.Idle) return;
        var mail = Mails[0];
        GameManager.Inst.CurrentState = GameManager.GameState.OpenMail;
        OpenMailWindow.SetCharacter(mail);
    }
    public void InteruptedMail()
    {
        OpenMailWindow.AnimateOpenClose(false);
        Notification.SetActive(Mails.Count != 0);
        NumberOfMails.text = Mails.Count.ToString();
    }
    public void CloseMail()
    {
        Mails.RemoveAt(0);
        OpenMailWindow.AnimateOpenClose(false);
        UpdateVisual(false);
    }

    public void AddMail()
    {
        if (Mails == null)
            Mails = new List<MailCharacter>();
        Mails.Add(GenerateCharacter.Inst.GenerateChar());
        UpdateVisual(true);
    }
    public void UpdateVisual(bool added)
    {
        if(added)
        {
            transform.DOLocalJump(transform.localPosition,40, 1, 0.6f);
            transform.DOShakeScale(0.6f);
        }
        else
        {
            Mappe.Animate();
            //do another sound
        }
        Notification.SetActive(Mails.Count != 0);
        NumberOfMails.text = Mails.Count.ToString();
    }

}
