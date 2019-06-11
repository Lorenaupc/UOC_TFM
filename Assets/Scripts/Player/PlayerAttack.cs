﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

    private int thrust;

	void Start () {
        thrust = 5;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                if (collision.tag.Equals("Enemy"))
                {
                    if (transform.tag.Equals("Player"))
                    {
                        Vector2 difference = hit.transform.position - transform.position;
                        difference = difference.normalized * thrust;
                        hit.AddForce(difference, ForceMode2D.Impulse);

                        hit.GetComponent<EnemyAI>().currentState = EnemyState.stagger;
                        collision.GetComponent<EnemyAI>().KnockHit(hit);

                        Debug.Log("Player");
                    }
                    else
                    {
                        Vector2 difference = hit.transform.position - transform.position;
                        difference = difference.normalized * 1;
                        hit.AddForce(difference, ForceMode2D.Impulse);

                        hit.GetComponent<EnemyAI>().currentState = EnemyState.stagger;
                        collision.GetComponent<EnemyAI>().KnockHitWithoutDamage(hit);
                        Debug.Log("Enemy");
                    }
                }

                if (collision.tag.Equals("Player"))
                {
                    Vector2 difference = hit.transform.position - transform.position;
                    difference = difference.normalized * thrust;
                    hit.AddForce(difference, ForceMode2D.Impulse);

                    hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                    collision.GetComponent<PlayerMovement>().KnockHit();
                }
            }
        }
    }
}
