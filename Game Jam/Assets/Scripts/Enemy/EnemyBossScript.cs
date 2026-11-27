using UnityEngine;

public class EnemyBossScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 playerPos;
    private Rigidbody2D myRb;

    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float health = 300;
    [SerializeField] private float attackCooldown = 2.5f;
    [SerializeField] private float damage = 40;
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
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        Vector3 currentPos = gameObject.transform.position;
        Vector2 direction = (playerPos - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        timeSinceAttack += Time.deltaTime;

        if (distanceToPlayer <= attackRange)
        {
            myRb.linearVelocity = Vector2.zero;

            if (canFireProjectiles)
            {
                RangedAttack(direction);
            }
            else
            {
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

        if (playerPos.x < transform.position.x && facingDirection == -1 ||
            playerPos.x > transform.position.x && facingDirection == 1)
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
            timeSinceAttack = 0f;

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
        health += amount;
    }
}
