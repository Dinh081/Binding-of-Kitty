using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private InputActionReference _movementInput;
    [SerializeField] private InputActionReference _shootInput;
    [SerializeField] private InputActionReference _menuControl;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _shootForce = 10f;
    private Vector2 _lastDirection = Vector2.left;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private int _health;
    [SerializeField] private UIUpdater _UI;
    private bool _isInvincible = false;
    private float _invincibilityDuration = 0.5f;
    private static bool _gotPowerUp = false;
    private static bool _enemyDestroyed = false;

    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _powerUpMessage;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _movementInput.action.Enable();
        _shootInput.action.Enable();
        _menuControl.action.Enable();
    }

    private void Start()
    {
        _UI.UpdateHealth(_health);
    }

    private void Update()
    {
        _movement = _movementInput.action.ReadValue<Vector2>();

        if (_movement != Vector2.zero)
        {
            _lastDirection = _movement.normalized;
        }

        if (_shootInput.action.triggered)
        {
            ShootProjectile(_lastDirection);
        }

        if (_menuControl.action.triggered)
        {
            Time.timeScale = 0f;
            _pauseMenu.SetActive(true);
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
            GameObject projectile = Instantiate(_projectilePrefab, _shootPoint.position, Quaternion.identity);
            Rigidbody2D projRb = projectile.GetComponent<Rigidbody2D>();

            if (projRb != null)
            {
                if (direction == Vector2.zero)
                {
                    direction = _lastDirection;
                }

                projRb.velocity = direction.normalized * _shootForce;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !_isInvincible)
        {
            _health--;
            _UI.UpdateHealth(_health);
            
            if (_gotPowerUp)  // after getting PowerUp enemy gets damage when he touches the player
            {
                EnemyBehavior enemy = collision.gameObject.GetComponent<EnemyBehavior>();
                enemy.DecreaseHealth(1);
            }

            if (_health <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else
            {
                StartCoroutine(InvincibilityCoroutine());
            }
        }
        else if (collision.gameObject.CompareTag("Door") && _enemyDestroyed is true) 
        {
            
                SceneManager.LoadScene("Level2");
                _enemyDestroyed = false;
        }
        else if (collision.gameObject.CompareTag("PowerUp"))
        {
            _powerUpMessage.SetActive(true);
            Destroy(collision.gameObject);
            _enemyDestroyed = true;
            _gotPowerUp = true;

        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        _isInvincible = true;
        yield return new WaitForSeconds(_invincibilityDuration);
        _isInvincible = false;
    }

    private void OnDisable()
    {
        _movementInput.action.Disable();
        _shootInput.action.Disable();
        _menuControl.action.Disable();
    }
}
