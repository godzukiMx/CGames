using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Singleton para LevelManager
    public static LevelManager sharedInstance;

    //Listas para los bloques de niveles, tanto el total como los que se han generado
    public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();

    // Inicio del nivel
    public Transform levelStartPosition;

    void Awake(){
        if (sharedInstance == null){
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateInitialBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Agtregar nuevo bloque
    public void AddLevelBLock(){

    }

    //Remover bloques ya usados
    public void RemoveLevelBlock(){

    }

    // Remover bloques al morir, para reiniciar la partida
    public void RemoveAllLevelBlocks(){

    }

    //Metodo para generar el primer bloque
    public void GenerateInitialBlocks(){
        for (int i = 0; i < 4; i++ ){
            AddLevelBLock();
        }
    }
}
