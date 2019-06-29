using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignDialog : MonoBehaviour {

    public string signText;
    public GameObject dialogBox;
    public int space;

    private float width;

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
                if (space != 0)
                {
                    width = dialogBox.GetComponent<RectTransform>().rect.width;
                    dialogBox.GetComponent<RectTransform>().sizeDelta = new Vector2(width + space, dialogBox.GetComponent<RectTransform>().rect.height);
                }
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
                if (space != 0)
                {
                    dialogBox.GetComponent<RectTransform>().sizeDelta = new Vector2(width - space, dialogBox.GetComponent<RectTransform>().rect.height);
                }
                dialogBox.SetActive(false);
            }
        }
    }
}
