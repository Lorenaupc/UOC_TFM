using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDeathEnemies : MonoBehaviour {

    public List<GameObject> enemies;
    private int enemiesDead;

    private void Start()
    {
        enemiesDead = 0;
    }

    void Update () {
		if (enemies != null)
        {
            enemiesDead = 0;
            foreach (GameObject enemy in enemies)
            {
                if (enemy == null)
                {
                    enemiesDead++;
                } 
            }

            if (enemiesDead != enemies.Count)
            {
                GetComponent<DisableTransitions>().Disable();
            }
            else
            {
                GetComponent<DisableTransitions>().Enable();
            }
        }
	}
}
