using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clock : MonoBehaviour
{
    public Transform Minute, Hour;

    public void UpdateClock(float time)
    {
        Minute.rotation = Quaternion.Euler(0, 0, -time * 6f);
        Hour.rotation = Quaternion.Euler(0, 0, -time / 2f);
    }

}
