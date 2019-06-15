using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GoblinEnemyState
{
    patrol,
    attack,
    stagger
}

public class GoblinEnemyAI : MonoBehaviour{

    public GoblinEnemyState currentState;

    public Transform[] points;
    internal int currentPoint;

    private bool deadGoblin;

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

    void Start()
    {
        deadGoblin = false;
        transform.position = points[0].position;
        currentPoint = 0;

        if (transform.localScale.x == 2)
        {
            currentState = GoblinEnemyState.patrol;
            animator = GetComponent<Animator>();
            target = GameObject.FindGameObjectWithTag("Player").transform;
            health = 30;
            attackHit = 2;
            speed = 1f;

            rb2d = GetComponent<Rigidbody2D>();

            chaseRadious = 30;
            attackRadious = 4f;
        }
        else if (transform.localScale.x == 0.5f)
        {
            currentState = GoblinEnemyState.patrol;
            animator = GetComponent<Animator>();
            target = GameObject.FindGameObjectWithTag("Player").transform;
            health = 3;
            attackHit = 1;
            speed = 3f;

            rb2d = GetComponent<Rigidbody2D>();

            chaseRadious = 10;
            attackRadious = 1f;
        }
    }

    void FixedUpdate()
    {
        checkDistance();
    }

    void checkDistance()
    {
        if (target != null && target.GetComponent<PlayerHealth>().health > 0)
        {
            if (Vector3.Distance(target.position, transform.position) <= chaseRadious && Vector3.Distance(target.position, transform.position) > attackRadious)
            {
                if (currentState == GoblinEnemyState.patrol && currentState != GoblinEnemyState.stagger)
                {
                    Vector3 temp = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                    rb2d.MovePosition(temp);
                    SetAnimatorsFloats(temp - transform.position);
                    currentState = GoblinEnemyState.patrol;
                }                
            }
            else if (Vector3.Distance(target.position, transform.position) <= attackRadious)
            {
                rb2d.velocity = Vector2.zero;
                StartCoroutine(Attack());
            }
            else if (Vector3.Distance(target.position, transform.position) > chaseRadious && !deadGoblin)
            {
                if (Vector3.Distance(transform.position, points[currentPoint].position) < 0.5f)
                {
                    currentPoint++;
                }

                if (currentPoint >= points.Length)
                {
                    currentPoint = 0;
                }
                
                Vector3 temp = Vector3.MoveTowards(transform.position, points[currentPoint].position, speed * Time.deltaTime);
                rb2d.MovePosition(temp);
                SetAnimatorsFloats(temp - transform.position);
                changeState(GoblinEnemyState.patrol);
            }
        }
    }

    public IEnumerator Attack()
    {
        changeState(GoblinEnemyState.attack);
        animator.SetBool("attack", true);
        yield return new WaitForSeconds(0.9f);
        animator.SetBool("attack", false);
        changeState(GoblinEnemyState.patrol);
    }

    private void SetAnimatorsFloats(Vector2 direction)
    {
        direction = direction.normalized;
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
    }

    private void changeState(GoblinEnemyState newState)
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

    public void KnockHit(Rigidbody2D hit, int attackPower)
    {
        StartCoroutine(Knock(hit, attackPower));
    }

    private IEnumerator KnockWithoutDamage(Rigidbody2D hit)
    {
        if (hit != null)
        {
            yield return new WaitForSeconds(0.2f);
            hit.velocity = Vector2.zero;

            yield return new WaitForSeconds(0.2f);
            currentState = GoblinEnemyState.patrol;
        }
    }

    private IEnumerator Knock(Rigidbody2D hit, int attackPower)
    {
        if (hit != null)
        {
            decreaseHealth(attackPower);

            yield return new WaitForSeconds(0.2f);
            hit.velocity = Vector2.zero;

            yield return new WaitForSeconds(0.2f);
            currentState = GoblinEnemyState.patrol;
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
        deadGoblin = true;
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        foreach (BoxCollider2D collider in colliders)
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
        if (transform.localScale.x == 0.5f)
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

            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().maximumHealth += 3;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().changeHealth(3);
        }
    }

}
