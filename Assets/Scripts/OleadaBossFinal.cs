using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OleadaBossFinal : MonoBehaviour {
    
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public Transform[] patrolPoints;
    public float repeatTime;
    public float numOleadas;
    private float currentOleadas;

    internal bool activada;
    public bool isColliding = false;

    private void Start()
    {
        activada = false;
    }

    private void Update()
    {
        isColliding = false;

        if (GetComponent<CheckDeathEnemies>().allDead)
        {
            GetComponent<DisableTransitions>().Enable();
            activada = false;
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
        
        InvokeRepeating("setOleadas", 0, repeatTime);
    }

    private void setOleadas()
    {
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
