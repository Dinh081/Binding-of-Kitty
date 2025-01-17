using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _uptime = 3f;
    private Rigidbody2D _rb;

    [SerializeField] Rigidbody2D hiddenDoorBody;

    public GameObject powerUp;
    private static bool _gotPowerUp = false;
    
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

                // --Destroy the enemy if health reaches 0-- instead load scene again <hier ColliderBox ausschalten, damit man in den neuen Raum kann (angenommen es gibt nur einen Gegner pro Raum)>
                if (enemy.GetHealth() <= 0)
                {
                    Destroy(collision.gameObject);
                    if (_gotPowerUp is false) {
                        Instantiate(powerUp, enemy.transform.position, Quaternion.identity); // spawns the power up on the place of enemy without rotation
                        _gotPowerUp = true;
                    }
                        
                    Destroy(hiddenDoorBody);
    
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
