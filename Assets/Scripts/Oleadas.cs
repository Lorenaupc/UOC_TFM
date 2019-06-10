using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oleadas : MonoBehaviour {

    public List<GameObject> fences;
    public List<Transform> spawnPoints;
    public GameObject enemyPrefab;
    public float repeatTime;
    public float numOleadas;
    private float currentOleadas;

    private void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0 && currentOleadas == numOleadas)
        {
            foreach (GameObject fence in fences)
            {
                fence.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            createOleada();
        }
    }

    private void createOleada()
    {
        Destroy(GetComponent<CircleCollider2D>());
        foreach (GameObject fence in fences)
        {
            fence.SetActive(true);
        }
        InvokeRepeating("setOleadas", 0, repeatTime);
    }

    private void setOleadas()
    {
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
        CancelInvoke();
    }
}
