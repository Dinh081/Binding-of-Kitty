using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private int _enemyHealth;
    [SerializeField] private UIUpdater _UI;
    private Rigidbody2D _rb;


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
        _UI.UpdateEnemyHealth(_enemyHealth);
    }
}
