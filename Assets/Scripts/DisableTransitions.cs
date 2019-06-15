using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTransitions : MonoBehaviour {

    public Transform[] transitions;

    internal void Disable()
    {
        foreach (Transform transition in transitions)
        {
            transition.GetComponent<Transitions>().enabled = false;
            transition.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    internal void Enable()
    {
        foreach (Transform transition in transitions)
        {
            transition.GetComponent<Transitions>().enabled = true;
            transition.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }
}
