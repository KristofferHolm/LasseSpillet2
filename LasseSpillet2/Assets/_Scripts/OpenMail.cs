using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using TMPro;
public class OpenMail : MonoBehaviour
{
    public MailCharacter CurrentMailCharacter;
    public TextMeshProUGUI Description;
    public TextMeshProUGUI BulletPoints;
    public TextMeshProUGUI ErhversErfaring;
    public AnimationCurve AnimateEntranceSlerp;
    public Transform UddannelsesNiveau;
    /*
    Navn: 
    Alder: 
    Adresse: 
    Uddannelsesniveau: 
    */
    public InsertImage Image;

    bool Opened = false;

    public CheckBox Checkbox;
    void Start()
    {
        GameManager.Inst.Lasse += Interupted;
        var vector0 = new Vector3(0, 1, 1);
        transform.localScale = vector0;
        Checkbox.transform.localScale = vector0;
    }
    public void Interupted()
    {
        if (Opened)
            PC.Inst.InteruptedMail();
    }
    public void SetCharacter(MailCharacter mailchar)
    {
        AnimateOpenClose(true);
        CurrentMailCharacter = mailchar;
        Checkbox.UnToggleAll();
        StringBuilder t = new StringBuilder();
        t.Append("Navn: " + mailchar.Navn + " " + mailchar.Efternavn);
        t.Append("\n");
        t.Append("Alder: " + mailchar.Alder.ToString());
        t.Append("\n");
        t.Append("Adresse: " + mailchar.Adresse + " " + mailchar.AdresseNum.ToString());
        t.Append("\n");
        t.Append(mailchar.Postnr.ToString());
        t.Append(" ");
        t.Append(mailchar.By);
        Description.text = t.ToString();
        t = new StringBuilder();
        for (int i = 0; i < 3; i++)
        {
            t.Append("- " + mailchar.Beskrivelser[i]);
            t.Append("\n");
        }
        BulletPoints.text = t.ToString();
        Image.InsertPicture(mailchar.Billede);
        for (int i = 0; i < UddannelsesNiveau.childCount; i++)
        {
            UddannelsesNiveau.GetChild(i).gameObject.SetActive(mailchar.UddanelseNiveau == i);
        }
        //mailchar.Exp
        t = new StringBuilder();
        t.Append("Erhvervserfaring: \n");
        foreach (var xp in mailchar.Exp)
        {
            t.Append("\n");
            t.Append(xp.årTilår);
            t.Append("\n");
            t.Append(xp.stilling);
            t.Append("\n");
            t.Append("____________________");
        }
        ErhversErfaring.text = t.ToString();
    }
    public void AnimateOpenClose(bool open)
    {
        Opened = open;
        StartCoroutine(AnimateEntrance(0.6f, open));
    }
    IEnumerator AnimateEntrance(float timeToAnimate, bool enter)
    {
        var vector0 = enter ? new Vector3(0, 1, 1) : Vector3.one;
        transform.localScale = vector0;
        Checkbox.transform.localScale= vector0;
        float timePassed = 0;
        while (timePassed<timeToAnimate)
        {
            timePassed += Time.deltaTime;
            var sizeX = AnimateEntranceSlerp.Evaluate((timePassed / timeToAnimate));
            if (!enter) sizeX = 1 - sizeX;
            vector0 = new Vector3(sizeX, 1, 1);
            transform.localScale = vector0;
            Checkbox.transform.localScale = vector0;
            yield return null;
        }
        vector0 = enter ? new Vector3(1, 1, 1) : new Vector3(0,1,1);
        transform.localScale = vector0;
        Checkbox.transform.localScale = vector0;
        
    }
}
