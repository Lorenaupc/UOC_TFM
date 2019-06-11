using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour {
    
	public void UseHealthPotion (int amount) {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().changeHealth(amount);
	}
}
