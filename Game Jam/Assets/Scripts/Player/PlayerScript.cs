using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D myRb;
    private Animator myAn;
    private float moveSpeed = 10f;
    private float takeDamageRate = 0.2f;
    private float enemyCollisionTimer = 0f;
    private bool isSliding = false;
    private float slideCooldown = 5f;
    private float slideDuration = 1f;
    private AudioSource source;


    [SerializeField] private float playerHealth = 100f;
    [SerializeField] private bool isDead = false;
    [SerializeField] private float slideDurationTimer = 0f;
    [SerializeField] private float slideCooldownTimer = 5f;
    [SerializeField] private AudioClip slideSound;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRb = gameObject.GetComponent<Rigidbody2D>();
        source = gameObject.GetComponent<AudioSource>();
        myAn = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        myRb.linearVelocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
        if (myRb.linearVelocity != Vector2.zero)
        {
            myAn.SetBool("isRun", true);
        }
        else
        {
            myAn.SetBool("isRun", false);
        }
        if (Input.GetKeyDown("space") && isSliding == false && slideCooldownTimer >= slideCooldown)
        {
            Slide();
            slideCooldownTimer = 0f;
        }

        if (isSliding == true)
        {
            slideDurationTimer += Time.deltaTime;
            if (slideDurationTimer >= slideDuration)
            {
                UndoSlide();
                slideDurationTimer = 0f;
            }
        }

        if (isSliding == false)
        {
            slideCooldownTimer += Time.deltaTime;
        }

    }

    void Slide()
    {
        isSliding = true;
        moveSpeed = 30f;
        source.clip = slideSound;
        source.Play();
        myAn.SetTrigger("isSlide");
    }

    void UndoSlide()
    {
        isSliding = false;
        moveSpeed = 10f;
    }


    // This function can be used for taking damage or for healing (damage should be negative)
    public void ChangeHealth(float amount)
    {
        if (amount < 0)
        {
            myAn.SetTrigger("isHurt");
        }
        playerHealth += amount;
        if (playerHealth <= 0)
        {
            myAn.SetTrigger("isDead");
            isDead = true;
            playerHealth = 0;
        }
    }

    public bool GetIsSliding()
    {
        return isSliding;
    }

    public float GetSlideDurationTimer()
    {
        return slideDurationTimer;
    }

    public float GetSlideCooldownTimer()
    {
        return slideCooldownTimer;
    }

    public float GetSlideCooldown()
    {
        return slideCooldown;
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
