using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameView : MonoBehaviour
{
    public Text scoreText, coinsText, maxScoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {   // Se lleva el conteo de los items recolectados, se muestra en el UI del juego
            int coins = GameManager.sharedInstance.collectedObject;
            float score = 0;
            float maxScore = 0;

            coinsText.text = coins.ToString();
            scoreText.text = "Score: " + score.ToString("f1");
            maxScoreText.text = "Max Score: " + maxScore.ToString("f1");
            }
    }
}
