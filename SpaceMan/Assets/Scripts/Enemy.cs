using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float runningSpeed = 1.5f;
    public bool facingRight = false;
    int enemyDamage = -10;

    Rigidbody2D rigidBody;
    private Vector3 starPosition;
    // Start is called before the first frame update
    
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        starPosition = this.transform.position;
    }
    
    void Start()
    {  
        this.transform.position = starPosition;
    }
    
    void FixedUpdate()
    {
        float currentRunningSpeed = runningSpeed;

        if(facingRight){
            //Mirando a la derecha
            currentRunningSpeed = runningSpeed;
            this.transform.eulerAngles = new Vector3(0, 180, 0);
        }else{
            //Mirando a la izquierda
            currentRunningSpeed = -runningSpeed;
            this.transform.eulerAngles = Vector3.zero;
        }

        if(GameManager.sharedInstance.currentGameState == GameState.inGame){
            rigidBody.velocity = new Vector2(currentRunningSpeed, rigidBody.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if(collision.tag == "Collectible"){
                return;
            }

            if(collision.tag == "Player"){
                collision.gameObject.GetComponent<PlayerController>().CollectHealth(enemyDamage);
                return;
            }
            // Al llegar a este punto la bala no ha colisionado con monedas ni el jugador
            // Lo mas probable es que aqui exista otro enemigo o el mismo escenario
            // Haremos que el enemigo gire
            facingRight = !facingRight;
    }
}
