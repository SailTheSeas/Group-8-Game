using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private PauseMenu pauseMenu;
    public void LoseGame()
    {
        pauseMenu.LoseGame();
        loseScreen.SetActive(true);
    }
}
