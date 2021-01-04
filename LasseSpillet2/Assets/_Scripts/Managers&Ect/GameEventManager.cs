using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager
{
    [Serializable]
    public struct GameEventCallback
    {
        public Action Action;
        public bool RemoveAfterCall;

        public override bool Equals(object obj)
        {
            if (obj is GameEventCallback callback)
            {
                return callback.Action == Action;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    private static List<GameEventCallback> _toRemove = new List<GameEventCallback>();
    private static Dictionary<GameEvent, List<GameEventCallback>> eventDic = new Dictionary<GameEvent, List<GameEventCallback>>();
    
    public static void AddListener(GameEvent _event, Action method, bool removeAfterCall = false)
    {
        if (!eventDic.ContainsKey(_event))
        {
            eventDic.Add(_event, new List<GameEventCallback>());
        }

        eventDic[_event].Add(new GameEventCallback { Action = method, RemoveAfterCall = removeAfterCall });
    }

    public static void RemoveListener(GameEvent _event, Action method)
    {
        GameEventCallback callback = new GameEventCallback { Action = method };

        if (eventDic.ContainsKey(_event))
        {
            if (eventDic[_event].Contains(callback))
            {
                eventDic[_event].Remove(callback);
            }
        }
    }
    public static void RemoveALLListeners()
    {
        eventDic.Clear();
    }

    public static void RaiseEvent(GameEvent _event)
    {
        if(eventDic.TryGetValue(_event, out List<GameEventCallback> list))
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Action();

                if (list[i].RemoveAfterCall)
                {
                    _toRemove.Add(list[i]);
                }
            }

            for(int i = 0; i < _toRemove.Count; i++)
            {
                list.Remove(_toRemove[i]);
            }

            _toRemove.Clear();
        }
        else
        {
            //Debug.Log("could not raise event: " + _event);
        }
    }
}

public enum GameEvent
{
    TrolleyAnimDone,
    Play_00VTcutsc02WagonFall,
    Play_00VTcutsc05KarlaFall,
    FirstEdgeHelp,
    Dia_KurtMissedJump,
    OneCharacterAtCustodian,
    FollowLightDone,
    Dia_KarlaSignificantJump,
    Dia_KurtNoUse,
    KarlaAndKurtExitedElevator,
    Play_00VTcutsc03CustodianHandle,
    Dia_OnlyOneReachedCustodian,
    Dia_NeedToReachCustodian,
    SwitchCharacter,
    EndOfCutscene01,
    Play_00VTcutsc04Kurt1stPower,
    KurtAtPowerZone2,
    KarlaAtTopTrolley,
    KarlaAtTop,
    ControllerTypeChanged,
    FirstEnteringLevel,
    FirstClimbStart,
    TopRightLadder,
    KarlaAtFirstTrolley,
    LeftOfDrawerLadder,
    KurtFirstTrolleyInteraction,
    FirstClimbDone,
    KarlaHalfwayUpRope,
    Play_00VTcutsc01Arrival,
    TriggerProgression,
    Cutscene05_BoyStillHere,
    Cutscene05_CustoVoice,

    Chapter09_Cutscene03_WalkAway,
    Play_Chapter09_Cutscene04B,

    Dia_KarlaGoodJob,
}