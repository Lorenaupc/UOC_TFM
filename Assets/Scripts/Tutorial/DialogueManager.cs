using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour {

    public Dialogue dialog;
    Queue<string> sentences;
    public GameObject player;

    public GameObject dialogPanel;
    public TextMeshProUGUI displayText;

    string activeSentence;
    public float typingSpeed;

    private void Start()
    {
        sentences = new Queue<string>();
        StartDialog();
    }

    void StartDialog()
    {
        sentences.Clear();

        player.GetComponent<PlayerMovement>().enabled = false;

        foreach(string sentence in dialog.sentenceList)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    void DisplayNextSentence()
    {
        if (sentences.Count <= 0)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            dialogPanel.SetActive(false);
            return;
        }

        activeSentence = sentences.Dequeue();
        displayText.text = activeSentence;

        StopAllCoroutines();
        StartCoroutine(TypeTheSentence(activeSentence));
    }

    IEnumerator TypeTheSentence(string sentence)
    {
        displayText.text = "";

        foreach(char letter in sentence.ToCharArray())
        {
            displayText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    internal void nextSentence()
    {
        if (displayText.text == activeSentence)
        {
            DisplayNextSentence();
        }
    }
}
