using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateGame : MonoBehaviour {
    
    public InputField inputf;

    public void Back()
    {
        SceneManager.LoadScene("Open");
    }

    public void Create()
    {
        if (inputf.text.Equals(""))
        {
            //Advertencia de que tienes que rellenar el nombre
        }
        else
        {
            PlayerPrefs.SetString("PlayerName", inputf.text);
            SceneManager.LoadScene("Farm");
        }
    }
}
