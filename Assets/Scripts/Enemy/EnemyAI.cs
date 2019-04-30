using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class EnemyAI : MonoBehaviour {

    public EnemyState currentState;

    internal int health;
    internal int attackHit;
    private float speed;

    private Transform target;
    private float chaseRadious;
    private float attackRadious;
    private Rigidbody2D rb2d;

    private Animator animator;

	void Start () {

        currentState = EnemyState.idle;
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        health = 3;
        attackHit = 1;
        speed = 2f;

        rb2d = GetComponent<Rigidbody2D>();

        chaseRadious = 4;
        attackRadious = 1.5f;
	}
	
	void FixedUpdate () {
        checkDistance();
	}

    void checkDistance()
    {
        if (Vector3.Distance(target.position,transform.position) <= chaseRadious && Vector3.Distance(target.position, transform.position) > attackRadious)
        {
            if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                rb2d.MovePosition(temp);
                SetAnimatorsFloats(temp - transform.position);
                changeState(EnemyState.walk);
                animator.SetBool("wakingUp", true);
            }
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadious)
        {
            animator.SetBool("wakingUp", false);
            //changeState(EnemyState.idle);
        }
    }

    private void SetAnimatorsFloats(Vector2 direction)
    {
        direction = direction.normalized;
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
    }

    private void changeState( EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }

    public void KnockHit(Rigidbody2D hit)
    {
        StartCoroutine(Knock(hit));
    }

    private IEnumerator Knock(Rigidbody2D hit)
    {
        if (hit != null)
        {
            yield return new WaitForSeconds(0.3f);
            hit.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            hit.velocity = Vector2.zero;
        }
    }
}
