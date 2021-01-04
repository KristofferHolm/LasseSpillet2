using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MailCharacter
{
    public string Navn, Efternavn, Adresse, By;
    public int Postnr;
    public int AdresseNum;
    public int Alder;
    public List<string> Beskrivelser;
    public int UddanelseNiveau;
    public string GetUddannelsesNiveau => Uddanelse.GetName(typeof(Uddanelse), UddanelseNiveau);
    public List<EvervsErfaring> Exp;
    public List<Sprite> Billede;

    public bool[] FitsInCheckbox = new bool[5];
    

    public enum Uddanelse
    {
        Grundskole,
        GymnasialUddannelse,
        ErhvervsfagligUddannelse,
        KortVideregåendeUddannelse,
        MellemLangVideregåendeUddannelse,
        LangVideregåendeUddannelse
    }
    public struct EvervsErfaring
    {
        public string stilling;
        public string årTilår;
        
    }
}