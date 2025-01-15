using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update

    public UnityEvent levelUp;
    public SpriteRenderer spriteRenderer;
    


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnHealthChanged(int newHealth)
    {
        if (newHealth == 0)
        {
            levelUp.Invoke();
        }
    }


    public void IncrementTimeScale(float value)
    {
        Time.timeScale += value;
    }





    public void OpenSesame(Sprite openDoor)
    {
     
        spriteRenderer.sprite = openDoor;
    }
     public void TroughWall(GameObject hiddenDoor)
    {
        Destroy(hiddenDoor);
    }



}

