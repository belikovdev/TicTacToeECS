using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScreen : Screen
{
    public Text text;

    public void SetWinner(CellState value)
    {
        switch(value)
        {
            case CellState.X:
                text.text = "Player X wins";
                break;
            case CellState.O:
                text.text = "Player O wins";
                break;
            default:
                text.text = "Draw";
                break;
        }
    }

    public void OnRestartClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
