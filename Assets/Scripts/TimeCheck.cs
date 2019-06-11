using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCheck : MonoBehaviour {
    
	void Start () {
        InvokeRepeating("IncreaseStamina", 0, 5);
	}
	
    private void IncreaseStamina()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEffort>().AddEffort(2);
    }
}
