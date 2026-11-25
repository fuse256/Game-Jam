using UnityEngine;

public class Enemy1Script : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 playerPos;
    private Rigidbody2D myRb;
    private float moveSpeed = 4f;

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
        myRb.linearVelocity = new Vector2((playerPos.x - currentPos.x) * moveSpeed, (playerPos.y - currentPos.y) * moveSpeed);
    }
}
