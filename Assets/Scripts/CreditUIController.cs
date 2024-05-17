using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditUIController : MonoBehaviour
{
    public Button mainmenuButton;

    private void Start()
    {
        mainmenuButton.onClick.AddListener(MainMenu);
    }

    private void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}