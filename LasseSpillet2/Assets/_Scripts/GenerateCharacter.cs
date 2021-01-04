using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class GenerateCharacter : Singleton<GenerateCharacter>
{
    public List<MailCharacter> MailCharacters;
    public BilledeGenerator BilledeGenerator;
    public AnimationCurve AlderAlgorithm;
    public AnimationCurve Arbejdsl�sAlgorithm;
    [FilePath]
    public string CSV2Read;
    int coronaNum = 87;

    string[] inputs = new string[]{
        "Navn",
        "Efternavn",
        "Adresse",
        "Beskrivelser",
        "Post nr",
        "By",
    };


    [Button]
    public MailCharacter GenerateChar()
    {
        MailCharacter newChar = new MailCharacter();
        newChar.FitsInCheckbox = new bool[5];
        var disc = CSVReader.Read();
        newChar.Alder = (int)AlderAlgorithm.Evaluate(Random.Range(0f, 1f));

        newChar.AdresseNum = Random.Range(1, 99);
        newChar.Beskrivelser = new List<string>();
        var PostNummerListIndex = -1;
        int i = 0;
        foreach (var keys in disc.Keys)
        {
            disc.TryGetValue(keys, out var list);
            int max = list.Count;
            switch (i)
            {
                case 0:
                    newChar.Navn = list[Random.Range(0, max)];
                    break;
                case 1:
                    newChar.Efternavn = list[Random.Range(0, max)];
                    break;
                case 2:
                    newChar.Adresse = list[Random.Range(0, max)];
                    break;
                case 3:
                    var listOfnums = new List<int>();
                    while (listOfnums.Count < 3)
                    {
                        var num = Random.Range(0, max);
                        if (!listOfnums.Contains(num))
                        {
                            if (num > coronaNum)
                            {
                                newChar.FitsInCheckbox[3] = true;
                                max = coronaNum;
                            }
                            listOfnums.Add(num);
                        }
                    }
                    foreach (var num in listOfnums)
                    {
                        newChar.Beskrivelser.Add(list[num]);
                    }
                    break;
                case 4:
                    if (PostNummerListIndex == -1)
                        PostNummerListIndex = Random.Range(0, max);
                    var poststring = list[PostNummerListIndex];
                    int.TryParse(poststring, out int result);
                    newChar.Postnr = result;
                    break;
                case 5:
                    if (PostNummerListIndex == -1)
                        PostNummerListIndex = Random.Range(0, max);
                    newChar.By = list[PostNummerListIndex];
                    break;
                case 6:
                    newChar.FitsInCheckbox[1] = GenerateWorkExp(newChar.Alder, list, out var xp);
                    newChar.Exp = xp;
                    break;
                default:
                    break;
            }
            i++;
        }
      

        int udd = Random.Range(0, 5);
        newChar.UddanelseNiveau = udd;
        newChar.Billede = new List<Sprite>();
        newChar.Billede = BilledeGenerator.GetRandomBillede();
        newChar.FitsInCheckbox[0] = newChar.Alder < 30;
        //newChar.FitsInCheckbox[1] = false; //not implemented yet.
        newChar.FitsInCheckbox[2] = udd>2;
        //newChar.FitsInCheckbox[3] set in switch
        newChar.FitsInCheckbox[4] = newChar.Postnr >= 8000 && newChar.Postnr < 9000;
        return newChar;
    }
    /// <summary>
    /// return true if Arbejsl�s siden 2015
    /// </summary>
    /// <param name="alder"></param>
    /// <param name="listOfJobs"></param>
    /// <param name="xp"></param>
    /// <returns></returns>
    private bool GenerateWorkExp(int alder, List<string> listOfJobs, out List<MailCharacter.EvervsErfaring> xp)
    {
        var randomNum = Random.Range(18, 25);
        var antalJobs = alder / randomNum;
        var �rstal = 2020 - alder;
        var alderspend = 0;
        var alderToSpend = alder - randomNum;
        var evervsErfarings = new List<MailCharacter.EvervsErfaring>();
        var arbejdsl�sI5�r = false;
        for (int i = 0; i < antalJobs; i++)
        {
            MailCharacter.EvervsErfaring entry = new MailCharacter.EvervsErfaring();
            entry.stilling = listOfJobs[Random.Range(0, listOfJobs.Count)];
            var start�r = �rstal + randomNum + alderspend;
            var �rIJob = alderToSpend / antalJobs + Random.Range(-10, 2);
            if (�rIJob < 0)
                �rIJob = 0;
            alderspend += �rIJob;
            var til�r = start�r + �rIJob;
            var til�rIText = til�r > 2020 ? "nu" : til�r.ToString();
            entry.�rTil�r = $"{start�r} - {til�rIText}";
            if (i == antalJobs - 1)
                arbejdsl�sI5�r = til�r < 2016;
            evervsErfarings.Add(entry);
        }
        xp = evervsErfarings;
        return arbejdsl�sI5�r;
    }
}
