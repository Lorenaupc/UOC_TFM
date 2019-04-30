using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectCard : MonoBehaviour {

    public GameObject loadInstance;
    public GameObject grid;

	void Start () {

        if (PlayerPrefs.HasKey("PlayerName"))
        {
            GameObject instantLoad = Instantiate(loadInstance, Vector3.zero, Quaternion.identity);
            Text[] texts = instantLoad.GetComponentsInChildren<Text>();
            foreach(Text childText in texts)
            {
                if (childText.transform.name.Equals("Time"))
                {
                    childText.text = PlayerPrefs.GetString("Time");
                }
                else
                {
                    childText.text = PlayerPrefs.GetString("PlayerName");
                }
            }
            instantLoad.GetComponent<Button>().onClick.AddListener(Clicked);
            instantLoad.transform.parent = grid.transform;
        }
    }

    public void Back()
    {
        SceneManager.LoadScene("Open");
    }

    public void Clicked()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("ActualScene"));
    }
}
