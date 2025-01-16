using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] InputActionReference _movementInput;
    [SerializeField] private InputActionReference _shootInput;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _shootPoint; // Point where the projectile spawns
    [SerializeField] private float _shootForce = 10f; // Force to apply to the projectile
    private Vector2 _lastDirection = Vector2.left; // Default direction (facing left)
    private Vector2 _movement;
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private int _health;
    [SerializeField] private UIUpdater _UI;

    private bool _isInvincible = false;
    private float _invincibilityDuration = 0.5f;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _movementInput.action.Enable();
        _shootInput.action.Enable();
    }

    void Start()
    {
        _UI.UpdateHealth(_health);
    }

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

        _UI.UpdateHealth(_health);
    }

    private void FixedUpdate()
    {
        _rb.velocity = _movement * _speed;
    }

    private void ShootProjectile(Vector2 direction)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isInvincible) return; // Prevent taking damage if the player is invincible

        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (_health < 2)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                _health--;
                _UI.UpdateHealth(_health);
                StartCoroutine(InvincibilityCoroutine());
            }
<<<<<<< Updated upstream
        else if (collision.gameObject.CompareTag("Door")) {
            SceneManager.LoadScene("Level2");
        }
=======
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        _isInvincible = true;

        yield return new WaitForSeconds(_invincibilityDuration);

        _isInvincible = false;
>>>>>>> Stashed changes
    }

    private void OnDisable()
    {
        _movementInput.action.Disable();
        _shootInput.action.Disable();
    }
}
