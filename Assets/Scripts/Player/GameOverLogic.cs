using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverLogic : MonoBehaviour
{
    public GameObject GameoverPanel;
   public void GameOver()
    {
        GameoverPanel.SetActive(true);
    }
}
