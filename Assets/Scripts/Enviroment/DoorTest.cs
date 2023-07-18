using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTest : Interactable
{
    public override void Interact()
    {
        base.Interact();
        gameObject.SetActive(false);
    }
}
