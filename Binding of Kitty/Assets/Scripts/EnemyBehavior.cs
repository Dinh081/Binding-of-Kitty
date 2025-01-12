using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private int _enemyHealth;
    [SerializeField] private UIUpdater _UI;
    private Rigidbody2D _rb;

    [SerializeField] private float _enemySpeed;
    public Transform _target;
    private Vector2 _moveDirection;

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
    }

    public int GetHealth()
    {
        return _enemyHealth;  // Get the current health of the enemy
    }

    // Update is called once per frame
    void Update()
    {
        if (_target)
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            _moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        if (_target)
        {
            _rb.velocity = new Vector2(_moveDirection.x, _moveDirection.y) * _enemySpeed;
        }

    }
}
