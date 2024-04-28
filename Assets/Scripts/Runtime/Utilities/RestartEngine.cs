using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartEngine : Interactable
{
    public bool keyCardSet { get; private set; } = false;
    public static event System.Action OnEngineRestarted;
    private void Awake()
    {
        SetKeyCard.OnKeyCardSet += SetKeyCard_OnKeyCardSet;
    }

    private void SetKeyCard_OnKeyCardSet()
    {
        keyCardSet = true;
    }

    override protected void Interact()
    {
        if (keyCardSet)
        {
            OnEngineRestarted?.Invoke();
        }
    }
}
