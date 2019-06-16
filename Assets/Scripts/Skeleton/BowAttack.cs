using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SkeletonEnemyState
{
    patrol,
    attack,
    stagger
}

public class BowAttack : MonoBehaviour {

    public SkeletonEnemyState currentState;

    public Transform[] points;
    internal int currentPoint;

    private bool deadSkeleton;

    internal int health;
    private float speed;

    private Transform target;
    private float attackRadious;
    private Rigidbody2D rb2d;

    private Animator animator;

    public GameObject projectilePrefab;

    public List<InventoryItem> lootableObjects;
    public GameObject lootedPrefab;

    void Start()
    {
        deadSkeleton = false;
        if (points.Length != 0)
        {
            transform.position = points[0].position;
        }
        else
        {
            attackRadious = 15;
        }

        currentPoint = 0;

        currentState = SkeletonEnemyState.patrol;
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        health = 5;
        speed = 1f;

        rb2d = GetComponent<Rigidbody2D>();

        attackRadious = 8;
    }

    void FixedUpdate()
    {
        checkDistance();
    }

    void checkDistance()
    {
        if (target != null && target.GetComponent<PlayerHealth>().health > 0)
        {
            if (Vector3.Distance(target.position, transform.position) <= attackRadious)
            {
                if (currentState == SkeletonEnemyState.patrol && currentState != SkeletonEnemyState.stagger)
                {
                    Vector3 temp = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                    animator.SetBool("attack", true);
                    SetAnimatorsFloats(temp - transform.position);
                    InvokeRepeating("ShootArrow", 0f, 2f);
                    changeState(SkeletonEnemyState.attack);
                    rb2d.bodyType = RigidbodyType2D.Static;
                }
            }
            else if (Vector3.Distance(target.position, transform.position) > attackRadious && !deadSkeleton)
            {
                animator.SetBool("attack", false);
                CancelInvoke("ShootArrow");
                rb2d.bodyType = RigidbodyType2D.Dynamic;

                if (points.Length != 0)
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
                }
                else
                {
                    Vector3 temp = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                    SetAnimatorsFloats(temp - transform.position);
                    rb2d.MovePosition(temp);
                }
                changeState(SkeletonEnemyState.patrol);
            }
        }
    }

    private void ShootArrow()
    {
        Vector3 temp = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        SetAnimatorsFloats(temp - transform.position);

        Vector3 tempVector = target.transform.position - transform.position;
        tempVector.Normalize();

        GameObject arrow = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        arrow.GetComponent<CanonProjectile>().speed = new Vector2(tempVector.x * 6, tempVector.y * 6);

        float rot_z = Mathf.Atan2(tempVector.y, tempVector.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 180);
    }

    private void SetAnimatorsFloats(Vector2 direction)
    {
        direction = direction.normalized;
        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
    }

    private void changeState(SkeletonEnemyState newState)
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

    public void KnockHit(Rigidbody2D hit, int attackPower, Vector2 difference)
    {
        StartCoroutine(Knock(hit, attackPower, difference));
    }

    private IEnumerator KnockWithoutDamage(Rigidbody2D hit)
    {
        if (hit != null)
        {
            CancelInvoke("ShootArrow");
            rb2d.bodyType = RigidbodyType2D.Dynamic;

            yield return new WaitForSeconds(0.2f);
            hit.velocity = Vector2.zero;

            yield return new WaitForSeconds(1f);
            currentState = SkeletonEnemyState.patrol;
        }
    }

    private IEnumerator Knock(Rigidbody2D hit, int attackPower, Vector2 difference)
    {
        if (hit != null)
        {
            CancelInvoke("ShootArrow");
            rb2d.bodyType = RigidbodyType2D.Dynamic;
            hit.AddForce(difference, ForceMode2D.Impulse);
            decreaseHealth(attackPower);

            yield return new WaitForSeconds(0.2f);
            hit.velocity = Vector2.zero;

            yield return new WaitForSeconds(1f);
            currentState = SkeletonEnemyState.patrol;
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
        attackRadious = 0;
        deadSkeleton = true;
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        foreach (BoxCollider2D collider in colliders)
        {
            collider.enabled = false;
        }
        yield return new WaitForSeconds(1f);
        
        LootObject();
        Destroy(this.gameObject);
    }

    private void LootObject()
    {
        int x = Random.Range(0, lootableObjects.Count);
        GameObject prefab = Instantiate(lootedPrefab, transform.position, Quaternion.identity);
        prefab.GetComponent<SpriteRenderer>().sprite = lootableObjects[x].itemImage;
        prefab.GetComponent<LootCollider>().item = lootableObjects[x];
        prefab.GetComponent<LootCollider>().item.count = 1;
        
    }

}
