using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    internal int health;
    internal int maximumHealth;
    internal string name;
    internal bool died;
    public TextMeshProUGUI hearts;
    internal int attackPower;
    private Color orange;

    private void Awake()
    {
        name = PlayerPrefs.GetString("PlayerName");
    }

    void Start () {
        health = maximumHealth = 4;
        attackPower = 1;
        died = false;

        hearts.text = "x" + health;
        orange = new Color(1,0.64f,0);
        hearts.color = orange;
    }

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Return"))
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<DialogueManager>().nextSentence();
        }
    }
        
    public void changeHealth(int amount)
    {
        switch (amount)
        {
            case 3:
                if (health < maximumHealth)
                {
                    health+=amount;
                    if (health >= maximumHealth)
                    {
                        health = maximumHealth;
                        hearts.color = orange;
                    }
                    hearts.text = "x" + health;
                }
                break;
            case 2:
                if (health < maximumHealth)
                {
                    health += amount;
                    if (health >= maximumHealth)
                    {
                        health = maximumHealth;
                        hearts.color = orange;
                    }
                    hearts.text = "x" + health;
                }
                break;
            case 1:
                if (health < maximumHealth)
                {
                    health++;
                    hearts.text = "x" + health;
                    if (health == maximumHealth)
                    {
                        hearts.color = orange;
                    }
                }
                break;
            case -1:
                if (health > 0)
                {
                    health--;
                }
                if (health.Equals(0))
                {
                    hearts.text = "x" + health;
                    hearts.color = Color.red;
                    GetComponent<SpriteRenderer>().color = Color.red;
                    died = true;
                    StartCoroutine(dead());
                }
                else
                {
                    hearts.color = Color.white;
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
        GetComponent<PlayerMovement>().animator.SetBool("death", true);
        yield return new WaitForSeconds(5f);
        Restart();
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return new WaitForSeconds(2f);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<Shops>().Start();
    }

    private void Restart()
    {
        transform.position = Vector3.zero;
        health = maximumHealth = 4;
        attackPower = 1;
        died = false;

        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<PlayerMovement>().animator.SetBool("death", false);

        hearts.text = "x" + health;
        orange = new Color(1, 0.64f, 0);
        hearts.color = orange;
    }
}
