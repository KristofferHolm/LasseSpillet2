using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalConstants : MonoSingleton<GlobalConstants>
{
    public bool ShowFPS = false;
    public bool HideScreenInfo = false;
    public bool ShowIntro = true;
    public float PushStrength = 10f;
}
