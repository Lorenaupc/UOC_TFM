using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour {

    static Singleton instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Home")
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.name == "ItemsFarm")
                {
                    foreach (Transform renderChild in child)
                    {
                        renderChild.GetComponent<SpriteRenderer>().enabled = false;
                        renderChild.GetComponent<BoxCollider2D>().enabled = false;
                    }
                }
                else{
                    child.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.name == "ItemsFarm")
                {
                    foreach (Transform renderChild in child)
                    {
                        renderChild.GetComponent<SpriteRenderer>().enabled = true;
                        renderChild.GetComponent<BoxCollider2D>().enabled = true;
                    }
                }
                else
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
