using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("No se toca hasta el final");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
