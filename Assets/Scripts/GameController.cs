using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject loseScreen;

    public void LoseGame()
    {
        loseScreen.SetActive(true);
    }
}
