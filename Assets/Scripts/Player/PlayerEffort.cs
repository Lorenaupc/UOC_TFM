using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEffort : MonoBehaviour {

    public Slider effortSlider;

    private void Start()
    {
        effortSlider.maxValue = 100;
        effortSlider.value = 100;
    }

    public void AddEffort(int amount)
    {
        if (effortSlider.value < 100)
        {
            effortSlider.value += amount;
            if (effortSlider.value > 100)
            {
                effortSlider.value = 100;
            }
        }
    }

    public void DecreaseEffort(int amount)
    {
        if (effortSlider.value > 5)
        {
            effortSlider.value -= amount;
            if (effortSlider.value < 5)
            {
                effortSlider.value = 5;
            }
        }
    }

    public bool EnoughEffort()
    {
        if (effortSlider.value > 5)
        {
            return true;
        }
        else return false;
    }

}
