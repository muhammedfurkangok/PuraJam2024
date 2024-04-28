using System.Collections;
using System.Collections.Generic;
using Runtime.Abstracts;
using UnityEngine;

public class Keypad : Interactable
{
    protected override void Interact()
    {
        ZombieSignals.Instance.OnZombiesAlerted?.Invoke();
    } 
}
