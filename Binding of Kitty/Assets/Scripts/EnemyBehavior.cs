using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private int _enemyHealth;
    [SerializeField] private int _maxEnemyHealth = 5;
    [SerializeField] private UIUpdater _UI;
    private Rigidbody2D _rb;

    [SerializeField] private float _enemySpeed;
    public Transform _target;
    private Vector2 _moveDirection;

    public float KBForce; // controlls how hard enemy knocks back 
    public float KBCounter; // counts how long the knockback lasts
    public float KBTotalTime; // the total time of the whole knockback effect

    public bool KnockFromRight;
    public bool KnockFromUp;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _enemyHealth = _maxEnemyHealth;
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
        if (_target && KBCounter <= 0)   // enemy can move only of there is a player and when knockback effect ends
        {
            _rb.velocity = new Vector2(_moveDirection.x, _moveDirection.y) * _enemySpeed;
        }
        else if (KBCounter > 0)
        {
            if (KnockFromRight)   // direction of knockback
            {
                _rb.velocity = new Vector2(-KBForce, KBForce);
            }
            if (KnockFromRight is false)
            {
                _rb.velocity = new Vector2(KBForce, KBForce);
            }
            if (KnockFromUp)
            {
                _rb.velocity = new Vector2(KBForce, -KBForce);
            }
            if (KnockFromUp is false)
            {
                _rb.velocity = new Vector2(KBForce, KBForce);
            }

            KBCounter -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
    }
}
