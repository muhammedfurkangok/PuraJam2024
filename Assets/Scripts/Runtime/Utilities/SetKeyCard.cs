using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Abstracts;
using UnityEngine;

public class SetKeyCard : Interactable
{
    public bool hasKeyCard { get; private set; }
    public static event System.Action OnKeyCardSet; //only one keycard in the game
    [SerializeField] GameObject keyCard;

    private void Awake()
    {
        GetKeyCard.OnKeyCardPickup += GetKeyCard_OnKeyCardPickup;
    }

    private void GetKeyCard_OnKeyCardPickup()
    {
        hasKeyCard = true;
    }

    protected override void Interact()
    {
        if (hasKeyCard)
        {
            keyCard.SetActive(true);
            OnKeyCardSet?.Invoke();
        }
    }
}
