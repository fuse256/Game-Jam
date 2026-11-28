using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
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
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private bool canFireProjectiles = false;
    [SerializeField] private float firePositionXOffset = 0.5f;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float attackDuration = 0.3f;
    [SerializeField] private bool spriteFacesRight = true;

    private float timeSinceAttack = 0f;
    

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

        // 0.1 is a buffer value, sometimes the enemy flipped direction for no reason (likely went beyond the player's x)
        bool playerIsToRight = playerPos.x > (transform.position.x + 0.1);

        timeSinceAttack += Time.deltaTime;

        if ((spriteFacesRight && (playerIsToRight && transform.localScale.x < 0 || !playerIsToRight && transform.localScale.x > 0)) ||
            (!spriteFacesRight && (playerIsToRight && transform.localScale.x > 0 || !playerIsToRight && transform.localScale.x < 0)))
        {
            Flip();
        }

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
                RangedAttack(direction);
                
            }
            else
            {
                StartCoroutine(MeleeAttack());
                
            }
        }
        else
        {
            ChasePlayer(direction);
        }
    }

    private void ChasePlayer(Vector2 direction)
    {
        myRb.linearVelocity = direction * moveSpeed;
    }

    private void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    IEnumerator MeleeAttack()
    {
        if (timeSinceAttack >= attackCooldown)
        {
            
            timeSinceAttack = 0f;

            if (player.TryGetComponent(out PlayerScript playerScript))
            {
                myAn.SetTrigger("isAttack");
                yield return new WaitForSeconds(attackDuration);
                float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
                    if (distanceToPlayer > attackRange)
                    {
                        yield break;
                    }
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
            myAn.SetTrigger("isAttack");
            // Vector3 projectileSpawn = transform.position + new Vector3(facingDirection * firePositionXOffset, transform.position.y);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            Instantiate(projectile, transform.position, rotation);

            timeSinceAttack = 0f;
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
