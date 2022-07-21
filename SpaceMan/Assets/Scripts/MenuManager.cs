using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Canvas menuCanvas, gameCanvas, gameOverCanvas;
    public static MenuManager sharedInstance;

    private void Awake()
    {
        if(sharedInstance == null){
            sharedInstance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Metodos para mostrar y ocultar el menu principal
    public void ShowMainMenu()
    {
        menuCanvas.enabled = true;
    }

    public void HideMainMenu()
    {
        menuCanvas.enabled = false;
    }

    // Metodo para salir del juego al rpesionar el boton "Quit" del menu principal
    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // Metodos para mostrar y ocultar el Score, Top Score y CoinCount
    public void ShowScoreUi()
    {
        
        gameCanvas.enabled = true;
    }

    public void HideScoreUi()
    {
        gameCanvas.enabled = false;
    }


    // Metodos para mostrar y ocultar el menu de GameOver
    public void ShowGameOverMenu()
    {
        gameOverCanvas.enabled = true;
    }

    public void HideGameOverMenu()
    {
        gameOverCanvas.enabled = false;
    }
}
