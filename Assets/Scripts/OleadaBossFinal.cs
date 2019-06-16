using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OleadaBossFinal : MonoBehaviour {
    
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public Transform[] patrolPoints;
    public float repeatTime;
    private float timeLeft;
    public float numOleadas;
    private float currentOleadas;
    private CircleCollider2D triggerCollider;
    public Text oleadaText;

    internal bool activada;
    public bool isColliding = false;

    private void Start()
    {
        activada = false;
        timeLeft = repeatTime + 1;
        triggerCollider = GetComponent<CircleCollider2D>();
        oleadaText.enabled = false;
    }

    public void RepeatedText()
    {
        timeLeft -= 1;
        if (timeLeft == 0)
        {
            timeLeft = repeatTime;
        }
        oleadaText.text = "Siguiente oleada en: " + Mathf.Round(timeLeft);
    }

    private void Update()
    {
        isColliding = false;

        if (GetComponent<CheckDeathEnemies>().allDead)
        {
            GetComponent<DisableTransitions>().Enable();
            activada = false;
            oleadaText.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isColliding)
        {
            isColliding = true;
            createOleada();
        }
    }

    private void createOleada()
    {
        Destroy(GetComponent<CircleCollider2D>());
        GetComponent<DisableTransitions>().Disable();

        activada = true;
        oleadaText.enabled = true;

        InvokeRepeating("RepeatedText", 0, 1);
        InvokeRepeating("setOleadas", 0, repeatTime);
    }

    private void setOleadas()
    {
        if (currentOleadas == numOleadas - 1)
        {
            CancelInvoke("RepeatedText");
            oleadaText.enabled = false;
        }

        if (currentOleadas < numOleadas)
        {
            foreach (Transform spawn in spawnPoints)
            {
                GameObject enemy = Instantiate(enemyPrefab, spawn.position, Quaternion.identity);
                enemy.GetComponent<BowAttack>().points = patrolPoints;
                GetComponent<CheckDeathEnemies>().enemies.Add(enemy);
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
