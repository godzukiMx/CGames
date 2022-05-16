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
        animator.SetBool(STATE_ALIVE, true);
        animator.SetBool(STATE_ON_THE_GROUND, true);
        animator.SetBool(IS_STATIC, false);
    }

    // Update is called once per frame
    void Update()
    {   // Detecta si se presiono la tecla para saltar y manda llamar el metodo de salto
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
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
    {   // Detecta el movimiento en base al eje indicado, agregando velocidad para mover al personaje
        rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * runningSpeed, rigidBody.velocity.y); 

        // Identifica hacia donde esta volteando el personaje y grira horizontalmente el sprite
        if (Input.GetAxis("Horizontal") < 0)
        {
            render.flipX = true;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            render.flipX = false;
        }
    }

    // Función  para realizar el salto
    void Jump()
    {
        if(isTouchingTheGround())
        {
        // Agregamos fuerzas vertical e impulso
        rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

        // Indica si el personaje toca o no el suelo
        bool isTouchingTheGround()
        {
            if (Physics2D.Raycast(this.transform.position,
                                  Vector2.down,
                                  1.5f,
                                  groundMask)){
                // TODO Programar logica de contacto con el suelo
                //animator.enabled = true;
                GameManager.sharedInstance.currentGameState = GameState.inGame;
                return true;                        
            }else{
                // TODO Programar logica de no contacto con el suelo
                //animator.enabled = false;
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
}