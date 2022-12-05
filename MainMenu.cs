using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button continueButton;

    private void Start()
    {
        continueButton.interactable = FileHandler.HasFile(MapCompletion.filename);
    }

    public void NewGame()
    {
        FileHandler.Reset(MapCompletion.filename);
        SceneManager.LoadScene(1);
    }
    
    public void ContinueGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
