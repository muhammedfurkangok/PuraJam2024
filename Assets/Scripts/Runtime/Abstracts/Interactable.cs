using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //player objeye baktiginda cikacak mesaj
    public string promptMessage;

    //bu fonksiyon cagirilacak
    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        //template
    }
}
