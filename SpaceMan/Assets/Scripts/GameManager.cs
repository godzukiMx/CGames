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
    private GameView scoreTextReset;

    public int collectedObject = 0;

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
        scoreTextReset = GameObject.Find("Game Canvas").GetComponent<GameView>();
        MenuManager.sharedInstance.HideScoreUi();
        MenuManager.sharedInstance.HideGameOverMenu();
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

    // Estado del juego usando enumeradores, cada metodo muestra y esconde canvas para mostrar u ocultar menus
    private void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            MenuManager.sharedInstance.ShowMainMenu();
            MenuManager.sharedInstance.HideScoreUi();
            MenuManager.sharedInstance.HideGameOverMenu();
        }
        else if (newGameState == GameState.inGame)
        {
            LevelManager.sharedInstance.RemoveAllLevelBlocks();
            LevelManager.sharedInstance.GenerateInitialBlocks();

            MenuManager.sharedInstance.HideMainMenu();
            MenuManager.sharedInstance.ShowScoreUi();
            MenuManager.sharedInstance.HideGameOverMenu();
            
            collectedObject = 0;
            scoreTextReset.scoreText.text = "Hello Motherfucker";
            controller.StarGame();
        }

        else if (newGameState == GameState.gameOver)
        {
            MenuManager.sharedInstance.HideMainMenu();
            MenuManager.sharedInstance.HideScoreUi();
            MenuManager.sharedInstance.ShowGameOverMenu();
        }

        this.currentGameState = newGameState;
    }
    // Metodo para llevar el conteo de cuantos item recolectables a tomado el jugador
    public void CollectObject(Collectable collectable)
    {
        collectedObject += collectable.value;
    }

}
