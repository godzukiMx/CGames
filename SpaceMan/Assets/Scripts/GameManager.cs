using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu, 
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{

    public GameState currentGameState = GameState.menu;

    public static GameManager sharedInstance;

    private PlayerController controller;

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit") && currentGameState != GameState.inGame){
            StartGame();
        }
    }

    // Metodo para iniciar el juego
    public void StartGame()
    {
        SetGameState(GameState.inGame);
    }

    // Metodo para finalizar el juego cuando el personaje muere
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    // Metodo para regresar al menu
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    // Estado del juego usando enumeradores
    private void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            // TODO: programar logica del menu
        }
        else if (newGameState == GameState.inGame)
        {
            controller.StarGame();
            // TODO: Preparar la escena para jugar
        }
        else if (newGameState == GameState.gameOver)
        {
            // TODO: preparar el juego para el game over
        }

        this.currentGameState = newGameState;
    }

}
