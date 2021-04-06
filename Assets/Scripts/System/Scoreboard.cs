using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] Text _txt_score_value, _txt_score_end;
    private int _pointValue;
    
    public void AddScore()
    {
        _pointValue++;
        _txt_score_value.text = "Score: " + _pointValue.ToString();
    }

    public void DisplayEndScreen()
    {
        _txt_score_value.enabled = false;
        _txt_score_end.text = "Score: " + _pointValue.ToString() + "\nPress [Enter] to play again";
    }
}
