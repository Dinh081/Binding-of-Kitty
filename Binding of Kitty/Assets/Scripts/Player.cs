using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] InputActionReference _movementInput;
    [SerializeField] private InputActionReference _shootInput;
    [SerializeField] private GameObject _projectilePrefab; // Reference to the projectile prefab
    [SerializeField] private Transform _shootPoint; // Point where the projectile spawns
    [SerializeField] private float _shootForce = 10f; // Force to apply to the projectile
    private Vector2 _lastDirection = Vector2.left; // Default direction (facing left)
    private Vector2 _movement;
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private int _health;
    [SerializeField] private UIUpdater _UI;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

    }
    private void OnEnable()
    {
        _movementInput.action.Enable();
        _shootInput.action.Enable(); // Enable shooting input
    }
    // Start is called before the first frame update
    void Start()
    {
        _UI.UpdateHealth(_health);
        
    }

    // Update is called once per frame
    void Update()
    {
        _movement = _movementInput.action.ReadValue<Vector2>();


        if (_movement != Vector2.zero)
        {
            _lastDirection = _movement.normalized;
        }

        // Shoot in the last direction when the shoot button is pressed
        if (_shootInput.action.triggered)
        {
            ShootProjectile(_lastDirection);
        }

    }

    private void FixedUpdate()
    {
        _rb.velocity = _movement * _speed;

    }
    private void ShootProjectile(Vector2 direction)
    {
        if (_projectilePrefab && _shootPoint)
        {
            if (_projectilePrefab && _shootPoint)
            {
                GameObject projectile = Instantiate(_projectilePrefab, _shootPoint.position, Quaternion.identity);
                Rigidbody2D projRb = projectile.GetComponent<Rigidbody2D>();

                if (projRb != null)
                {
                    if (direction == Vector2.zero)
                    {
                        direction = _lastDirection;
                    }

                    projRb.velocity = direction.normalized * _shootForce; // Shoot in the last known direction
                }

            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            if (_health < 2)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                _health--;
                _UI.UpdateHealth(_health);

            }
    }

    private void OnDisable()
    {
        _movementInput.action.Disable();
        _shootInput.action.Disable(); // Disable shooting input
    }

}

