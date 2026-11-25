using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D myRb;
    private float moveSpeed = 10f;
    [SerializeField] private float playerHealth = 100f;
    [SerializeField] private bool isDead = false;
    private float takeDamageRate = 0.2f;
    private float enemyCollisionTimer = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        myRb.linearVelocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
    }

    void TakeDamage(float x)
    {
        playerHealth = playerHealth - x;
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

        enemyCollisionTimer += Time.deltaTime;

        if (collider.gameObject.tag == "Enemy" && enemyCollisionTimer >= takeDamageRate)
        {
            TakeDamage(10);
            enemyCollisionTimer = 0f;
        }
    }

    public float GetPlayerHealth()
    {
        return playerHealth;
    }
}
