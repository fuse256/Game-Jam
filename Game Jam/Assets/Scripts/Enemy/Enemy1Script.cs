using Unity.VisualScripting;
using UnityEngine;

public class Enemy1Script : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 playerPos;
    private Rigidbody2D myRb;
    private Animator myAn;

    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float health = 100;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private float damage = 10;
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private bool canFireProjectiles = false;
    [SerializeField] private float firePositionXOffset = 0.5f;
    [SerializeField] private GameObject projectile;

    private float timeSinceAttack = 0f;
    private int facingDirection = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRb = gameObject.GetComponent<Rigidbody2D>();
        myAn = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        Vector3 currentPos = gameObject.transform.position;
        Vector2 direction = (playerPos - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        timeSinceAttack += Time.deltaTime;

        if (myRb.linearVelocity == Vector2.zero)
        {
            myAn.SetBool("isWalking", false);
        }
        else
        {
            myAn.SetBool("isWalking", true);
        }

        if (distanceToPlayer <= attackRange)
        {
            myRb.linearVelocity = Vector2.zero;

            if (canFireProjectiles)
            {
                myAn.SetTrigger("isAttack");
                RangedAttack(direction);
                
            }
            else
            {
                myAn.SetTrigger("isAttack");
                MeleeAttack();
                
            }
        }
        else
        {
            ChasePlayer(direction);
        }
    }

    private void ChasePlayer(Vector2 direction)
    {

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

    private void RangedAttack(Vector2 direction)
    {
        if (!canFireProjectiles || projectile == null)
        {
            return;
        }

        if (timeSinceAttack >= attackCooldown)
        {
            // Vector3 projectileSpawn = transform.position + new Vector3(facingDirection * firePositionXOffset, transform.position.y);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Instantiate(projectile, transform.position, rotation);

            timeSinceAttack = timeSinceAttack % attackCooldown;
        }
    }

    public void ChangeHealth(float amount)
    {
        if (amount < 0)
        {
            myAn.SetTrigger("Hurt");
        }
        health += amount;
        if (health <= 0)
        {
            myAn.SetTrigger("isDead");
        }
    }
}
