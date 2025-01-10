using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _uptime = 3f;
    private Rigidbody2D _rb;
    [SerializeField] private int _enemyHealth = 3;
    [SerializeField] private UIUpdater _UI;
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

                // Update the health UI (make sure _UI.UpdateHealth() is implemented correctly)
                _UI.UpdateHealth(enemy.GetHealth());

                // Destroy the enemy if health reaches 0
                if (enemy.GetHealth() <= 0)
                {
                    Destroy(collision.gameObject);  // Destroy the enemy
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
