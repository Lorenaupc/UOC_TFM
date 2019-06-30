using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyboardMenu : MonoBehaviour {


	void Start () {
        StartCoroutine(passScene());
	}
	
	private IEnumerator passScene()
    {
        yield return new WaitForSeconds(8f);
        SceneManager.LoadScene("Map");
    }
}
