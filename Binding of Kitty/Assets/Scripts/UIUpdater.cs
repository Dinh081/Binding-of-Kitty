using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIUpdater : MonoBehaviour
{
    [SerializeField] TMP_Text _EnemyHealth;
    [SerializeField] TMP_Text _health;
    public void UpdateEnemyHealth(int newEnemyHealth)
    {
        _EnemyHealth.text = "Enemy Health: " + newEnemyHealth.ToString();
    }

    public void UpdateHealth(int newHealth)
    {
        _health.text = "Health: " + newHealth.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
