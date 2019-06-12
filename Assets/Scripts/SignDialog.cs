using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignDialog : MonoBehaviour {

    public string signText;
    public GameObject dialogBox;

    private void Start()
    {
        dialogBox = GameObject.FindGameObjectWithTag("Canvas").GetComponent<DialogBoxManager>().dialogBox;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dialogBox != null)
        {
            if (collision.tag.Equals("Player"))
            {
                dialogBox.SetActive(true);
                dialogBox.GetComponentInChildren<Text>().text = signText;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (dialogBox != null)
        {
            if (collision.tag.Equals("Player"))
            {
                dialogBox.SetActive(false);
            }
        }
    }
}
