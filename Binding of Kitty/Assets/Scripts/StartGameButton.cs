using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameButton : MonoBehaviour
{
    public void LoadLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level1");
    }
}
