using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodEat : MonoBehaviour {

    public void EatFood(int amount)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEffort>().AddEffort(amount);
    }
}
