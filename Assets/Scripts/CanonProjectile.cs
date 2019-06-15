using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonProjectile : MonoBehaviour {

    public float moveSpeed;
    public float lifeTime;
    private float lifeTimeSeconds;
    private Rigidbody2D myRb;

    internal Vector2 speed;
    internal int attackPower;

	void Start () {
        lifeTimeSeconds = lifeTime;
        attackPower = 1;
        myRb = GetComponent<Rigidbody2D>();
        Shoot();
	}
	
	void Update () {
        lifeTimeSeconds -= Time.deltaTime;
        if (lifeTimeSeconds <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    public void Shoot()
    {
        myRb.velocity = speed * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") || collision.name.Equals("Boss"))
        {
            Destroy(this.gameObject);
        }
    }
}
