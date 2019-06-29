using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueAlert : MonoBehaviour {

    //Singleton
    static DialogueAlert instance;

    public Dialogue dialog;
    Queue<string> sentences;

    private GameObject dialogPanel;
    private TextMeshProUGUI displayText;

    string activeSentence;
    public float typingSpeed;

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

    private void Start()
    {
        sentences = new Queue<string>();
        dialogPanel = GameObject.FindGameObjectWithTag("GameManager").GetComponent<DialogueManager>().dialogPanel;
        displayText = GameObject.FindGameObjectWithTag("GameManager").GetComponent<DialogueManager>().displayText;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            dialogPanel.SetActive(true);
            StartDialog();
        }
    }

    void StartDialog()
    {
        sentences.Clear();

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
            dialogPanel.SetActive(false);
            gameObject.SetActive(false);
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
