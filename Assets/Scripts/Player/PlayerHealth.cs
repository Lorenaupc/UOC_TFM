using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    internal int health;
    internal string name;
    internal bool died;
    public Text hearts;
    internal int attackPower;

    private void Awake()
    {
        name = PlayerPrefs.GetString("PlayerName");
    }

    void Start () {
        health = 4;
        attackPower = 1;
        died = false;

        hearts.text = "x" + health;
	}
	
	public void changeHealth(int amount)
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
                    hearts.text = "x" + health;
                    GetComponent<SpriteRenderer>().color = Color.red;
                    died = true;
                    StartCoroutine(dead());
                }
                else
                {
                    GetComponent<SpriteRenderer>().color = Color.red;
                    StartCoroutine(redBlink());
                    hearts.text = "x" + health;
                }
                break;
        }
    }

    private IEnumerator redBlink()
    {
        Color end = Color.white;
        Color start = Color.red;
        for (float t = 0f; t < 1; t += Time.deltaTime)
        {
            float normalizedTime = t / 0.8f;
            GetComponent<SpriteRenderer>().color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        GetComponent<SpriteRenderer>().color = end;
    }

    private IEnumerator dead()
    {
        GetComponent<PlayerMovement>().animator.SetTrigger("death");
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
