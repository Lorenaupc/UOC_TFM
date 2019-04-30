using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    internal int health;
    internal string name;
    private bool die;
    public Text hearts;

    private void Awake()
    {
        name = PlayerPrefs.GetString("PlayerName");
    }

    void Start () {
        health = 4;
        die = false;

        hearts.text = "x" + health;
	}
	
	void changeHealth(int amount)
    {
        switch (amount)
        {
            case 1:
                health++;
                hearts.text = "x" + health;
                break;
            case -1:
                health--;
                if (health.Equals(0))
                {
                    die = true;
                }
                else
                {
                    hearts.text = "x" + health;
                }
                break;
        }
    }
}
