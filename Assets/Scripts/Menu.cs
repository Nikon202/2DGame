using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void Startbt()
    {
        SceneManager.LoadScene(1);
    }

    public void Exitbt()
    {
        Application.Quit();
    }
}
