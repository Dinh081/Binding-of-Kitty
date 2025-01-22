using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private int _enemyHealth;
    [SerializeField] private UIUpdater _UI;
    private Rigidbody2D _rb;
    public GameHandler gameHandler;
    public Transform _target;
    [SerializeField] private float _speed = 1f;
    private float _fleeDuration = 0.8f;  // Time the enemy moves away
    [SerializeField] private float _fleeSpeed = 2f;  // Speed at which the enemy moves away

    private bool _isFleeing = false;  // Flag to check if the enemy is currently fleeing


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _UI.UpdateEnemyHealth(GetHealth());
    }
    public void DecreaseHealth(int amount)
    {
        _enemyHealth -= amount;  // Reduce health by the specified amount
        gameHandler.OnHealthChanged(GetHealth());
        _UI.UpdateEnemyHealth(GetHealth());
    }

    public int GetHealth()
    {
        return _enemyHealth;  // Get the current health of the enemy
    }

    // Update is called once per frame

    void Update()
    {

        // If the enemy is fleeing, move away from the player
        if (_isFleeing)
        {
            // Calculate direction away from the player
            Vector2 awayFromPlayer = (transform.position - _target.position).normalized;

            // Move the enemy away from the player (move towards a position offset from current position)
            transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + awayFromPlayer, _fleeSpeed * Time.deltaTime);
        }
        else
        {
            // Otherwise, move toward the player
            transform.position = Vector2.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }

        _UI.UpdateEnemyHealth(_enemyHealth);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Start fleeing behavior for a short duration
            StartCoroutine(FleeFromPlayer());
        }
    }

    private IEnumerator FleeFromPlayer()
    {
        // Set the fleeing flag to true
        _isFleeing = true;

        // Wait for the specified duration while the enemy is fleeing
        yield return new WaitForSeconds(_fleeDuration);

        // After the duration, stop fleeing and allow normal movement
        _isFleeing = false;
    }
}
