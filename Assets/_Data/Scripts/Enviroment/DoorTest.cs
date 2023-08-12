using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTest : Interactable
{
    public override void Interact(InteractionController interactor)
    {
        base.Interact(interactor);
        gameObject.SetActive(false);
    }
}
