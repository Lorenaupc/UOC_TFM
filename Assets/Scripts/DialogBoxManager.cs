using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
