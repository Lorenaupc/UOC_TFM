using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canons : MonoBehaviour {

    public GameObject projectilePrefab;
    public Vector2 speed;
    public GameObject oleadaActivada;

    bool once = true;

	void Update () {

        if (oleadaActivada.GetComponent<Oleadas>().activada)
        {
            if (once)
            {
                InvokeRepeating("ShootProjectiles", 0, 5);
                once = false;
            }
        }

        if (!oleadaActivada.GetComponent<Oleadas>().activada)
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
