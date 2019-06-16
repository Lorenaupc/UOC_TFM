using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Oleadas : MonoBehaviour {

    public List<GameObject> fences;
    public List<Transform> spawnPoints;
    public GameObject enemyPrefab;
    public float repeatTime;
    private float timeLeft;
    public float numOleadas;
    private float currentOleadas;
    internal Text oleadaText;

    internal bool activada;

    bool once = true;
    bool isColliding = false;

    private void Start()
    {
        activada = false;
        timeLeft = repeatTime+1;
    }

    public void RepeatedText()
    {
        timeLeft -= 1;
        if (timeLeft == 0)
        {
            timeLeft = repeatTime;
        }
        oleadaText.GetComponent<Text>().enabled = true;
        oleadaText.text = "Siguiente oleada en: " + Mathf.Round(timeLeft);
    }

    private void Update()
    {
        isColliding = false;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        bool killed = true;
        foreach (GameObject enemy in enemies)
        {
            if (enemy.name.Equals("Enemy(Clone)") || enemy.name.Equals("EnemyBig(Clone)") || enemy.name.Equals("Boss(Clone)"))
            {
                killed = false;
            }
        }
        if (killed && currentOleadas == numOleadas)
        {
            GetComponent<DisableTransitions>().Enable();
            activada = false;
            foreach (GameObject fence in fences)
            {
                fence.SetActive(false);
            }
            //activate shop
            if (once)
            {
                if (transform.name.Equals("OleadaForest2"))
                {
                    GameObject.FindGameObjectWithTag("Canvas").GetComponent<DialogBoxManager>().RandomMessage("Has desbloqueado la tienda de frutas!");
                    GameObject.FindGameObjectWithTag("GameManager").GetComponent<Shops>().fruitsShop.SetActive(true);
                    GameObject.FindGameObjectWithTag("GameManager").GetComponent<Shops>().activateFruitShop = true;
                }
                else if (transform.name.Equals("OleadaForest3"))
                {
                    GameObject.FindGameObjectWithTag("Canvas").GetComponent<DialogBoxManager>().RandomMessage("Has desbloqueado la tienda de semillas!");
                    GameObject.FindGameObjectWithTag("GameManager").GetComponent<Shops>().seedsShop.SetActive(true);
                    GameObject.FindGameObjectWithTag("GameManager").GetComponent<Shops>().activateSeedShop = true;
                }
                else if (transform.name.Equals("OleadaForest4"))
                {
                    GameObject.FindGameObjectWithTag("Canvas").GetComponent<DialogBoxManager>().RandomMessage("Has desbloqueado la tienda de pociones!");
                    GameObject.FindGameObjectWithTag("GameManager").GetComponent<Shops>().potionsShop.SetActive(true);
                    GameObject.FindGameObjectWithTag("GameManager").GetComponent<Shops>().activatePotionShop = true;
                }
                once = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isColliding)
        {
            isColliding = true;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<Shops>().oleadatext.SetActive(true);
            oleadaText = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Shops>().oleadatext.GetComponent<Text>();
            createOleada();
        }
    }

    private void createOleada()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<DisableTransitions>().Disable();
        activada = true;
        foreach (GameObject fence in fences)
        {
            fence.SetActive(true);
        }
        InvokeRepeating("RepeatedText",0,1);
        InvokeRepeating("setOleadas", 0, repeatTime);
    }

    private void setOleadas()
    {
        if (currentOleadas == numOleadas - 1)
        {
            CancelInvoke("RepeatedText");
            oleadaText.GetComponent<Text>().enabled = false;
        }

        if (currentOleadas < numOleadas)
        {
            foreach(Transform spawn in spawnPoints)
            {
                Instantiate(enemyPrefab, spawn.position, Quaternion.identity);
            }

            currentOleadas++;
        }
        else
        {
            stopOleadas();
        }
    }

    public void stopOleadas()
    {
        CancelInvoke("setOleadas");
    }
}
