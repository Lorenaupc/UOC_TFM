using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public List<InventoryItem> lootableObjects;
    public GameObject lootedPrefab;

	void Start () {

        if (transform.localScale.x == 2)
        {
            currentState = EnemyState.idle;
            animator = GetComponent<Animator>();
            target = GameObject.FindGameObjectWithTag("Player").transform;
            health = 5;
            attackHit = 2;
            speed = 2f;

            rb2d = GetComponent<Rigidbody2D>();

            chaseRadious = 8;
            attackRadious = 0.1f;
        }
        else if (transform.localScale.x == 1)
        {
            currentState = EnemyState.idle;
            animator = GetComponent<Animator>();
            target = GameObject.FindGameObjectWithTag("Player").transform;
            health = 3;
            attackHit = 1;
            speed = 3f;

            rb2d = GetComponent<Rigidbody2D>();

            chaseRadious = 10;
            attackRadious = 0.5f;
        }     
        else if (transform.localScale.x == 7)
        {
            currentState = EnemyState.idle;
            animator = GetComponent<Animator>();
            target = GameObject.FindGameObjectWithTag("Player").transform;
            health = 100;
            attackHit = 3;
            speed = 1f;

            rb2d = GetComponent<Rigidbody2D>();

            chaseRadious = 20;
            attackRadious = 1f;
        }
	}
	
	void FixedUpdate () {
        checkDistance();
	}

    void checkDistance()
    {
        if (target != null && target.GetComponent<PlayerHealth>().health > 0)
        {
            if (Vector3.Distance(target.position, transform.position) <= chaseRadious && Vector3.Distance(target.position, transform.position) > attackRadious)
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
            }
        }
        else
        {
            changeState(EnemyState.idle);
            animator.SetBool("wakingUp", false);
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

    public void KnockHitWithoutDamage(Rigidbody2D hit)
    {
        StartCoroutine(KnockWithoutDamage(hit));
    }

    public void KnockHit(Rigidbody2D hit)
    {
        StartCoroutine(Knock(hit));
    }

    private IEnumerator KnockWithoutDamage(Rigidbody2D hit)
    {
        if (hit != null)
        {
            yield return new WaitForSeconds(0.3f);
            hit.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            hit.velocity = Vector2.zero;
        }
    }

    private IEnumerator Knock(Rigidbody2D hit)
    {
        if (hit != null)
        {
            yield return new WaitForSeconds(0.3f);
            hit.velocity = Vector2.zero;
            currentState = EnemyState.idle;
            hit.velocity = Vector2.zero;

            decreaseHealth(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().attackPower);
        }
    }

    public void decreaseHealth(int attackPower)
    {
        health -= attackPower;
        if (health == 0)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(dead());
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(redBlink());
        }
    }

    private IEnumerator redBlink()
    {
        Color end = Color.white;
        Color start = Color.red;
        for (float t = 0f; t < 1; t += Time.deltaTime)
        {
            float normalizedTime = t / 0.8f;
            GetComponent<SpriteRenderer>().color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        GetComponent<SpriteRenderer>().color = end;
    }

    private IEnumerator dead()
    {
        animator.SetTrigger("death");
        chaseRadious = 0;
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        foreach(BoxCollider2D collider in colliders)
        {
            collider.enabled = false;
        }
        yield return new WaitForSeconds(1f);

        //Loot object
        LootObject();
        Destroy(this.gameObject);
    }

    private void LootObject()
    {
        if (transform.localScale.x == 1)
        {
            int x = Random.Range(0, lootableObjects.Count);
            GameObject prefab = Instantiate(lootedPrefab, transform.position, Quaternion.identity);
            prefab.GetComponent<SpriteRenderer>().sprite = lootableObjects[x].itemImage;
            prefab.GetComponent<LootCollider>().item = lootableObjects[x];
            prefab.GetComponent<LootCollider>().item.count = 1;
        }
        else if (transform.localScale.x == 2)
        {
            int x = Random.Range(0, lootableObjects.Count);
            GameObject prefab = Instantiate(lootedPrefab, transform.position, Quaternion.identity);
            prefab.GetComponent<SpriteRenderer>().sprite = lootableObjects[x].itemImage;
            prefab.GetComponent<LootCollider>().item = lootableObjects[x];
            prefab.GetComponent<LootCollider>().item.count = 1;
        }
        else if (transform.localScale.x == 7)
        {
            //FALTA PONER UNA LLAVE LOOTEABLE
            int x = Random.Range(0, lootableObjects.Count);
            GameObject prefab = Instantiate(lootedPrefab, transform.position, Quaternion.identity);
            prefab.GetComponent<SpriteRenderer>().sprite = lootableObjects[x].itemImage;
            prefab.GetComponent<LootCollider>().item = lootableObjects[x];
        }
    }

}
