using UnityEngine;

public class Enemy1Script : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 playerPos;
    private Rigidbody2D myRb;

    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private int health = 100;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float damage = 10;
    [SerializeField] private float attackRange = 1.2f;

    private float timeSinceAttack = 0f;
    private int facingDirection = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        Vector3 currentPos = gameObject.transform.position;
        timeSinceAttack += Time.deltaTime;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        
        if (distanceToPlayer <= attackRange)
        {
            myRb.linearVelocity = Vector2.zero;
            MeleeAttack();
        }
        else
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
        Vector2 direction = (playerPos - transform.position).normalized;

        if (playerPos.x > transform.position.x && facingDirection == -1 ||
            playerPos.x < transform.position.x && facingDirection == 1)
        {
            Flip();
        }

        myRb.linearVelocity = direction * moveSpeed;
    }

    private void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    private void MeleeAttack()
    {
        if (timeSinceAttack >= attackCooldown)
        {
            timeSinceAttack = timeSinceAttack % attackCooldown;

            if (player.TryGetComponent(out PlayerScript playerScript))
            {
                playerScript.ChangeHealth(-damage);
                Debug.Log("Enemy hit player");
            }
        }
    }
}
