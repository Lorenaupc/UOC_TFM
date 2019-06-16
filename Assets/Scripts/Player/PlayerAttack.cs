using System.Collections;
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

                        if (hit.name.Contains("Goblin"))
                        {
                            hit.AddForce(difference, ForceMode2D.Impulse);
                            hit.GetComponent<GoblinEnemyAI>().currentState = GoblinEnemyState.stagger;
                            collision.GetComponent<GoblinEnemyAI>().KnockHit(hit, transform.GetComponentInParent<PlayerHealth>().attackPower);
                        }
                        else if (hit.name.Contains("Skeleton"))
                        {
                            Vector2 diff = hit.transform.position - transform.position;
                            diff = diff.normalized;
                            hit.GetComponent<BowAttack>().currentState = SkeletonEnemyState.stagger;                          
                            collision.GetComponent<BowAttack>().KnockHit(hit, transform.GetComponentInParent<PlayerHealth>().attackPower, diff);
                        }
                        else
                        {
                            hit.AddForce(difference, ForceMode2D.Impulse);
                            hit.GetComponent<EnemyAI>().currentState = EnemyState.stagger;
                            collision.GetComponent<EnemyAI>().KnockHit(hit, transform.GetComponentInParent<PlayerHealth>().attackPower);
                        }
                    }
                    else if (transform.tag.Equals("Enemy"))
                    {
                        Vector2 difference = hit.transform.position - transform.position;
                        difference = difference.normalized * 1;

                        if (hit.name.Contains("Goblin"))
                        {
                            hit.AddForce(difference, ForceMode2D.Impulse);
                            hit.GetComponent<GoblinEnemyAI>().currentState = GoblinEnemyState.stagger;
                            collision.GetComponent<GoblinEnemyAI>().KnockHitWithoutDamage(hit);
                        }
                        else if (hit.name.Contains("Skeleton"))
                        {
                            hit.GetComponent<BowAttack>().currentState = SkeletonEnemyState.stagger;
                            collision.GetComponent<BowAttack>().KnockHitWithoutDamage(hit);
                        }
                        else
                        {
                            hit.AddForce(difference, ForceMode2D.Impulse);
                            hit.GetComponent<EnemyAI>().currentState = EnemyState.stagger;
                            collision.GetComponent<EnemyAI>().KnockHitWithoutDamage(hit);
                        }
                    }
                    else if (transform.tag.Equals("Projectile"))
                    {
                        if (hit.name.Contains("Boss"))
                        {
                            hit.GetComponent<EnemyAI>().currentState = EnemyState.stagger;
                            collision.GetComponent<EnemyAI>().KnockHit(hit, GetComponent<CanonProjectile>().attackPower);
                        }
                    }
                }

                if (collision.tag.Equals("Player"))
                {
                    if (!collision.gameObject.GetComponent<PlayerHealth>().died)
                    {
                        if (!transform.name.Contains("Skeleton"))
                        {
                            Vector2 difference = hit.transform.position - transform.position;
                            if (transform.name.Contains("Goblin"))
                            {
                                difference = difference.normalized * 2;
                            }
                            else
                            {
                                difference = difference.normalized * thrust;
                            }
                            hit.AddForce(difference, ForceMode2D.Impulse);
                            hit.GetComponent<PlayerMovement>().currentState = PlayerState.stagger;
                            collision.GetComponent<PlayerMovement>().KnockHit();
                        }
                    }
                }
            }
        }
    }
}
