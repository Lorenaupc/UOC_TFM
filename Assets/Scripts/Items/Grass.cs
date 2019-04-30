using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour {

    public Sprite grassObject;

	void Start () {
        InvokeRepeating("SpawnGrass", 0, 5);
	}
	
	void SpawnGrass()
    {
        Vector3 randomPosition = Random.insideUnitCircle * 15;

        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position, randomPosition);
        Debug.Log(hit.collider.tag);
        if (hit.collider.tag == "NormalGrass")
        {
            hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = grassObject;
            hit.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
            hit.collider.tag = "Grass";
        }
    }
}
