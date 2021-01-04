using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenInfo : MonoSingleton<OnScreenInfo>
{
    private static bool hide = false;
    private int fontSize = 18;
    private int startx = 20;
    private int starty = 20;
    private List<string> LoggingFiles = new List<string>();
    private Dictionary<string, object> FilesToLog = new Dictionary<string, object>();

    public void AddInfoToScreen(string ID, object value)
    {
#if DEBUG
        // Don't even spend time on adding the info, if we are hiding it anyway
        if (FilesToLog.ContainsKey(ID))
        {
            FilesToLog[ID] = value;
            return;
        }
        FilesToLog.Add(ID, value);
#endif
    }
    void OnGUI()
    {
#if UNITY_STANDALONE && !UNITY_EDITOR
        return;
#endif

        GUIStyle style = GUI.skin.GetStyle("Label");

        style.normal.textColor = Color.red;
        style.fontSize = fontSize;
        style.alignment = TextAnchor.UpperLeft;
        style.wordWrap = false;
        
        Rect boundingBox = new Rect(startx, starty, 400, 30);
        Rect boundingBox2 = new Rect(startx, starty + 30, 400, 30);
        
        int i = 0;

        foreach (var files in FilesToLog)
        {
            if(files.Value == null)
            {
                GUI.Label(new Rect(startx, starty + 30 + 30 * i, 400, 30), "null", style);
                //Debug.LogError(files.Key + " is null");
                continue;
            }
            var log = files.Key + " : " + files.Value.ToString();
            GUI.Label(new Rect(startx, starty + 30 + 30 * i, 400, 30), log, style);
            i++;
        }
    }
}

