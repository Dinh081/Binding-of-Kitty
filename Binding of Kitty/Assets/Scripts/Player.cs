using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
    
public class Player : MonoBehaviour
{
    [SerializeField] InputActionReference _movementInput;
    private Vector2 _movement;
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

    }
    private void OnEnable()
    {
        _movementInput.action.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _movement = _movementInput.action.ReadValue<Vector2>();

    }
    private void FixedUpdate()
    {
        _rb.velocity = _movement * _speed;

    }
    private void OnDisable()
    {
        _movementInput.action.Disable();
    }
}

