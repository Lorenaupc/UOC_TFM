using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerState
{
    idle,
    walking,
    attacking,
    stagger
}

public class PlayerMovement : MonoBehaviour {

    public PlayerState currentState;
    private Rigidbody2D rigidbody2d;
    private readonly float velocity = 4.5f;
    internal Animator animator;
    private Vector3 change;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("PlayerPositionX"))
        {
            Vector3 playerPosition = Vector3.zero;
            playerPosition = new Vector3(PlayerPrefs.GetFloat("PlayerPositionX"), PlayerPrefs.GetFloat("PlayerPositionY"), PlayerPrefs.GetFloat("PlayerPositionZ"));
            transform.position = playerPosition;
        }

        if (PlayerPrefs.HasKey("PlayerHealth"))
        {
            GetComponent<PlayerHealth>().health = PlayerPrefs.GetInt("PlayerHealth");
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentState = PlayerState.walking;
    }

    private void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Attack") && currentState != PlayerState.attacking && currentState != PlayerState.stagger && !GetComponent<PlayerHealth>().died)
        {
            StartCoroutine(Attack());
        }

        if (currentState == PlayerState.walking || currentState == PlayerState.idle && !GetComponent<PlayerHealth>().died)
        {
            UpdateAnimationAndMovement();
        }
    }

    private IEnumerator Attack()
    {
        if (GetComponent<PlayerEffort>().EnoughEffort())
        {
            animator.SetBool("attacking", true);
            GetComponent<PlayerEffort>().DecreaseEffort(10);
            currentState = PlayerState.attacking;
            yield return null;
            animator.SetBool("attacking", false);
            yield return new WaitForSeconds(0.15f);
            currentState = PlayerState.walking;
        }
        else
        {
            Debug.Log("NOT STAMINA");
        }
    }

    void UpdateAnimationAndMovement()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        rigidbody2d.MovePosition(transform.position + change * velocity * Time.deltaTime);
    }

    public void KnockHit()
    {
        StartCoroutine(Knock());
    }

    private IEnumerator Knock()
    {
        if (rigidbody2d != null)
        {
            yield return new WaitForSeconds(0.3f);
            rigidbody2d.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            rigidbody2d.velocity = Vector2.zero;

            //mio
            GetComponent<PlayerHealth>().changeHealth(-1);
        }
    }
}
