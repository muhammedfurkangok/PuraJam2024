using Runtime.Abstracts;
using UnityEngine;

public class GetKeyCard : Interactable
{
    public static event System.Action OnKeyCardPickup; //only one keycard in the game

    [SerializeField] GameObject keyCard;
    protected override void Interact()
    {
        OnKeyCardPickup?.Invoke();
        keyCard.SetActive(false);
    }
}
