using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWinCondition : MonoBehaviour
{
    // put this class onto enterexit door
    //public static GameWinCondition instance;

    //public bool canWin { get; private set; } = false;

    //private void Awake()
    //{
    //    instance = this;
    //    RestartEngine.OnEngineRestarted += RestartEngine_OnEngineRestarted;
    //}

    //private void RestartEngine_OnEngineRestarted()
    //{
    //    canWin = true;
    //}

    //override protected void Interact()
    //{
    //    if (canWin)
    //    {
    //        OnGameWin?.Invoke();
    //    }
    //}

    public static event Action OnGameWin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnGameWin?.Invoke();
        }
    }


}
