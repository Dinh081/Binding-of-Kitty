using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameButton : MonoBehaviour
{
    public void ExitGame()
    {   
        #if UNITY_EDITOR
           UnityEditor.EditorApplication.isPlaying = false;
        #endif
            Application.Quit();
    }
}
