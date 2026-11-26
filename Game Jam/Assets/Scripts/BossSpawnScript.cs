using UnityEngine;

public class BossSpawnScript : MonoBehaviour
{
    [SerializeField] private GameObject enemyBoss;

    private float bossSpawnRate = 20f;

    private float bossTimer = 0f;
    [SerializeField] private GameObject enemies;
    private Rigidbody2D enemyRb;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bossTimer += Time.deltaTime;

        if (bossTimer >= bossSpawnRate)
        {
            GameObject newBoss = Instantiate(enemyBoss,gameObject.transform.position, Quaternion.identity, enemies.transform);
            bossTimer = 0f;
        }
    }
}
