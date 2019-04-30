using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {

	public void selectNewGame()
    {
        SceneManager.LoadScene("NewGame");
    }

    public void Continue()
    {
        SceneManager.LoadScene("SelectCard");
    }

    public void Options()
    {
        SceneManager.LoadScene("Options");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
