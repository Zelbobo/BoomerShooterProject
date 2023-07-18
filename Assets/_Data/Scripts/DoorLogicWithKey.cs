using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogicWithKey : MonoBehaviour
{
    public void OpenDoor()
    {
        if (KeyChain.PlayerGotRedKey == true)
        {
            gameObject.GetComponent<Animator>().Play("Open");
        }
    }

}
