﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IterateLateUpdate : MonoSingleton<IterateLateUpdate>
{
    List<Action> _list = new List<Action>();
    List<Action> _listContinues = new List<Action>();
    WaitForEndOfFrame endFrame = new WaitForEndOfFrame();

    public void Add(Action func)
    {
        _list.Add(func);
    }
    public void AddContinues(Action func)
    {
        _listContinues.Add(func);
    }
    public void RemoveContinues(Action func)
    {
        if (_listContinues.Contains(func))
            _listContinues.Remove(func);
        else
            Debug.LogError("The action: [ " + func.ToString() + " ] was not found, to be removed!");
    }
    public void EndOfFrame(Action action)
    {
        StartCoroutine(EndOfFrameRoutine(action));
    }

    private IEnumerator EndOfFrameRoutine(Action action)
    {
        yield return endFrame;
        action.Invoke();
    }

    private void LateUpdate()
    {
        foreach (var func in _listContinues)
        {
            func.Invoke();
        }

        if(_list.Count > 0)
        {
            foreach (var func in _list)
            {
                func.Invoke();
            }

            _list.Clear();
        }
    }
}
