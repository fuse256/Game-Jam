using UnityEngine;

public class EnemySpawnScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject enemy;
    private float spawnRate = 10f;
    private float timer = 0f;
    private Rigidbody2D enemyRb;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            GameObject newEnemy = Instantiate(enemy,gameObject.transform.position, Quaternion.identity);
            enemyRb = newEnemy.gameObject.GetComponent<Rigidbody2D>();
            enemyRb.linearVelocity = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
            timer = 0f;
        }
    }
}
