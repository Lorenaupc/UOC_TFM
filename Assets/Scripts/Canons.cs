using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canons : MonoBehaviour {

    public GameObject projectilePrefab;
    public Vector2 speed;

	void Start () {
        InvokeRepeating("ShootProjectiles", 0, 5);
	}
	
	void Update () {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().died)
        {
            CancelInvoke();
        }
	}

    private void ShootProjectiles()
    {
        GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        instance.GetComponent<CanonProjectile>().speed = speed;
    }
}
