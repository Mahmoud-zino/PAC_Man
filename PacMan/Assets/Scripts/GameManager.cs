using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool IsGameOver { get; private set; }

    public void GameOver()
    {
        this.IsGameOver = true;
        //save score and stuff
    }
}
