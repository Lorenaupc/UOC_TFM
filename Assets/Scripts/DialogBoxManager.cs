using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogBoxManager : MonoBehaviour {

    internal GameObject dialogBox;

    private void Awake()
    {
        dialogBox = GameObject.FindGameObjectWithTag("DialogBox");
    }

    private void Start()
    {
        dialogBox.SetActive(false);
    }

    public void RandomMessage(string message)
    {
        StartCoroutine(ShowMessage(message));
    }

    private IEnumerator ShowMessage(string message)
    {
        dialogBox.GetComponentInChildren<Text>().text = message;
        dialogBox.SetActive(true);
        yield return new WaitForSeconds(5f);
        dialogBox.SetActive(false);
    }
}

