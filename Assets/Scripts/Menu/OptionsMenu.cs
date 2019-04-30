using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

    public Scrollbar soundEffect;
    public Scrollbar music;

    public void Back () {
        SceneManager.LoadScene("Open");
    }
	
	public void Save () {
        SceneManager.LoadScene("Open");
    }

    public void changeSoundEffect()
    {
        //soundEffect.value;
    }

    public void changeMusic()
    {
        //music.value;
    }
}
