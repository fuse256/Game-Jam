using UnityEngine;

public class Projectile1Script : MonoBehaviour
{
    private Rigidbody2D myRb;
    [SerializeField] private float speed;
    [SerializeField] private float lifeTime;
    [SerializeField] private float damage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRb = gameObject.GetComponent<Rigidbody2D>();
        myRb.linearVelocity = transform.right * speed;  // Shoot in the direction the object is facing

        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            return;
        }
        if (other.TryGetComponent(out PlayerScript player))
        {
            player.ChangeHealth(-damage);
            Destroy(gameObject);
            Debug.Log("Projectile hit player");
            return;
        }
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}