using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private int _win;
    private int _lose;
    public Text textWin;
    public Text textLose;
    void Start()
    {
        _win = PlayerPrefs.GetInt("Win");
        _lose = PlayerPrefs.GetInt("Lose");
        RenderStatusText();
    }
    
    private void RenderStatusText()
    {
        textWin.text = "Win: " + _win;
        textLose.text = "Lose: " + _lose;
    }
}
