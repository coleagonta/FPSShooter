using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider slider;
    public int playerHealth = 100;
    public GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        slider.value = playerHealth;    
    }

    public void TakePlayerDamage(int playerDamage)
    {
        playerHealth = playerHealth - playerDamage;
        if (playerHealth <= 0)
        {
            gameManager.ChangeGameState(GameManager.GameState.Lose);
        }
    }
}
