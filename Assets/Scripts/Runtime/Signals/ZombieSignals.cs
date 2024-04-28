using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ZombieSignals : MonoBehaviour
{
    #region Singleton

    public static ZombieSignals Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    #endregion

    public UnityAction OnZombiesAlerted = delegate { };
}
