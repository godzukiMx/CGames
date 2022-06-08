using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Investigar sobre singleton con multijugadores.

public class PlayerController : MonoBehaviour
{

    public static PlayerController controllerInstace;

    //Variables del movimiento del personaje
    public float jumpForce = 7f;
    public float runningSpeed = 4f;
    Rigidbody2D rigidBody;
    Animator animator;
    SpriteRenderer render;
    Vector3 startPosition;
    
    const string STATE_ALIVE =  "isAlive";
    const string STATE_ON_THE_GROUND = "isOnTheGround";

    const string IS_STATIC = "isStatic";
    public LayerMask groundMask;

    void Awake() 
    {
        if (controllerInstace == null)
        {
            controllerInstace = this;
        }

        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        startPosition = this.transform.position;
    }
    // Inicio y reinicio del juego
    public void StarGame(){
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
        animator.SetBool(IS_STATIC, true);

        Invoke("RestardPosition", 0.2f);

    }

    void RestardPosition(){
        this.transform.position = startPosition;
        this.rigidBody.velocity = Vector2.zero;

        GameObject mainCamera = GameObject.Find("Main Camera");
        mainCamera.GetComponent<CameraFollow>().ResetCameraPosition();
    }


    // Update is called once per frame
    void Update()
    {   // Detecta si se presiono la tecla para saltar y manda llamar el metodo de salto
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        // CAmbios booleanos para indicar si esta en el suelo o si esta estatico y cambiar las animaciones necesarias
        animator.SetBool(STATE_ON_THE_GROUND, isTouchingTheGround());
        animator.SetBool(IS_STATIC, isStatic());

        Debug.DrawRay(this.transform.position, Vector2.down * 1.5f, Color.red);
    }

    // Update fijo, no se llama cada frame
     void FixedUpdate()    
    {   if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {// Detecta el movimiento en base al eje indicado, agregando velocidad para mover al personaje
            rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * runningSpeed, rigidBody.velocity.y);
            rigidBody.gravityScale = 1; 
            
            // Identifica hacia donde esta volteando el personaje y grira horizontalmente el sprite
            if (Input.GetAxis("Horizontal") < 0)
            {
                render.flipX = true;
            }
            if (Input.GetAxis("Horizontal") > 0)
            {
                render.flipX = false;
            }
        }else{ // Si no se esta dentro de la partida
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            rigidBody.gravityScale = 0;
        }
    }

    // Función  para realizar el salto
    void Jump()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame){
            if(isTouchingTheGround())
            {
            // Agregamos fuerzas vertical e impulso
            rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

        // Indica si el personaje toca o no el suelo
        bool isTouchingTheGround()
        {
            if (Physics2D.Raycast(this.transform.position,
                                  Vector2.down,
                                  1.5f,
                                  groundMask)){
                return true;                        
            }else{
                return false;
            }
        }

        // Indica si el personaje esta en movimiento o estatico
        bool isStatic()
        {
            if (Input.GetAxis("Horizontal") == 0)
            {
                return true;
            }else
            {
                return false;
            }
        }

        public void Die(){
            this.animator.SetBool(STATE_ALIVE, false);
            GameManager.sharedInstance.GameOver();
        }
}