using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {

    static PauseManager instance;

    private GameObject player;
    private bool isPaused;
    public GameObject pausePanel;

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

    void Start () {
        isPaused = false;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update () {
		if (Input.GetButtonDown("PauseMenu"))
        {
            activePauseMenu();
        }
	}

    private void activePauseMenu()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Resume()
    {
        activePauseMenu();
    }

    public void Quit()
    {
        SceneManager.LoadScene("Open");
        Time.timeScale = 1f;
    }

    public void Save()
    {
        PlayerPrefs.SetString("Time", System.DateTime.Now.ToString());

        PlayerPrefs.SetFloat("PlayerPositionX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPositionY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", player.transform.position.z);

        PlayerPrefs.SetInt("PlayerHealth", player.GetComponent<PlayerHealth>().health);

        PlayerPrefs.SetString("PlayerName", player.GetComponent<PlayerHealth>().name);

        PlayerPrefs.SetString("ActualScene", SceneManager.GetActiveScene().name);

        Resume();
        //Faltaria el dinero, inventario y cosechas
    }

    public void Load()
    {

        if (PlayerPrefs.HasKey("ActualScene"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetString("ActualScene"));
            activePauseMenu();
        }

        if (PlayerPrefs.HasKey("PlayerPositionX"))
        {
            Vector3 playerPosition = Vector3.zero;
            playerPosition = new Vector3(PlayerPrefs.GetFloat("PlayerPositionX"), PlayerPrefs.GetFloat("PlayerPositionY"), PlayerPrefs.GetFloat("PlayerPositionZ"));
            player.transform.position = playerPosition;
        }

        if (PlayerPrefs.HasKey("PlayerHealth"))
        {
            player.GetComponent<PlayerHealth>().health = PlayerPrefs.GetInt("PlayerHealth");
        }

       
    }
}
