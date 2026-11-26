using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D myRb;
    private float moveSpeed = 10f;
    [SerializeField] private float playerHealth = 100f;
    [SerializeField] private bool isDead = false;
    private float takeDamageRate = 0.2f;
    private float enemyCollisionTimer = 0f;
    private bool isDashing = false;
    private float dashCooldown = 5f;
    [SerializeField] private float dashDurationTimer = 0f;
    [SerializeField] private float dashCooldownTimer = 5f;
    private float dashDuration = 1f;
    private AudioSource source;
    [SerializeField] private AudioClip dashSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRb = gameObject.GetComponent<Rigidbody2D>();
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        myRb.linearVelocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
        if (Input.GetKeyDown("space") && isDashing == false && dashCooldownTimer >= dashCooldown)
        {
            Dash();
            dashCooldownTimer = 0f;
        }

        if (isDashing == true)
        {
            dashDurationTimer += Time.deltaTime;
            if (dashDurationTimer >= dashDuration)
            {
                UndoDash();
                dashDurationTimer = 0f;
            }
        }

        if (isDashing == false)
        {
            dashCooldownTimer += Time.deltaTime;
        }

    }

    void Dash()
    {
        isDashing = true;
        moveSpeed = 30f;
        source.clip = dashSound;
        source.Play();
    }

    void UndoDash()
    {
        isDashing = false;
        moveSpeed = 10f;
    }


    // This function can be used for taking damage or for healing (damage should be negative)
    public void ChangeHealth(float amount)
    {
        playerHealth += amount;
        if (playerHealth <= 0)
        {
            isDead = true;
        }
    }

    public bool GetDead()
    {
        return isDead;
    }

    void OnCollisionStay2D (Collision2D collider) {
        /*
        enemyCollisionTimer += Time.deltaTime;

        if (collider.gameObject.tag == "Enemy" && enemyCollisionTimer >= takeDamageRate)
        {
            ChangeHealth(-10);
            enemyCollisionTimer = 0f;
        }
        */
    }

    public float GetPlayerHealth()
    {
        return playerHealth;
    }
}
