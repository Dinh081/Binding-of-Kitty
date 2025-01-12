using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _uptime = 3f;
    private Rigidbody2D _rb;

    public EnemyBehavior enemyBehavior;

    // Start is called before the first frame update
    void Start()
    {
        CircleCollider2D collider = GetComponent<CircleCollider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }

        Destroy(gameObject, _uptime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the projectile collided with an enemy
        if (collision.CompareTag("Enemy"))
        {
            // If the enemy has health, reduce health and update UI
            EnemyBehavior enemy = collision.gameObject.GetComponent<EnemyBehavior>();
            if (enemy != null)
            {
                enemy.DecreaseHealth(1);  // Decrease health by 1 (adjust as needed)

                // Destroy the enemy if health reaches 0
                if (enemy.GetHealth() <= 0)
                {
                    Destroy(collision.gameObject);  // Destroy the enemy
                }

                enemyBehavior.KBCounter = enemyBehavior.KBTotalTime;
                if (collision.transform.position.x <= transform.position.x)  // enemy is on the left - hit from the right
                {
                    enemyBehavior.KnockFromRight = true;
                }
                if (collision.transform.position.x >= transform.position.x)  // enemy is on the right - hit from the left
                {
                    enemyBehavior.KnockFromRight = false;
                }
                if (collision.transform.position.y <= transform.position.y)  // enemy is down - hit from the top
                {
                    enemyBehavior.KnockFromUp = true;
                }
                if (collision.transform.position.y >= transform.position.y)  // enemy is up - hit from the bottom
                {
                    enemyBehavior.KnockFromUp = false;
                }

               
            }

            // Destroy the projectile
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
