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

    //Agtregar nuevo bloque de manera aleatoria en base al total de bloques
    public void AddLevelBLock(){
        int randomIdx = Random.Range(0, allTheLevelBlocks.Count);

        LevelBlock block;
        Vector3 spawnPosition = Vector3.zero;

        // Se genera el primer bloque del nivel
        if(currentLevelBlocks.Count == 0){
            block = Instantiate(allTheLevelBlocks[0]);
            spawnPosition = levelStartPosition.position;
        }else{
            block = Instantiate(allTheLevelBlocks[randomIdx]);
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count-1].endPoint.position;
        }

        block.transform.SetParent(this.transform, false);

        Vector3 correction = new Vector3(
            spawnPosition.x-block.startPoint.position.x,
            spawnPosition.y-block.startPoint.position.y,
            0);

        block.transform.position = correction;
        currentLevelBlocks.Add(block);

    }

    //Remover bloques ya usados
    public void RemoveLevelBlock(){
        LevelBlock oldBlock = currentLevelBlocks[0];
        currentLevelBlocks.Remove(oldBlock);
        Destroy(oldBlock.gameObject);
    }

    // Remover bloques al morir, para reiniciar la partida
    public void RemoveAllLevelBlocks(){
        while(currentLevelBlocks.Count>0){
            RemoveLevelBlock();
        }
    }

    //Metodo para generar el primer bloque
    public void GenerateInitialBlocks(){
        for (int i = 0; i < 3; i++ ){
            AddLevelBLock();
        }
    }
}
