using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	public static LevelGenerator sharedInstance; //Instancia compartida para solo tener un generador de niveles
	public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>();//Lista que contiene todos los niveles que se han creado
	public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();//Lista de los bloques que tenemos en pantalla
	public Transform levelInitialPoint;//Punto inicial donde empezara a crearse el primer nivel de todos
	private bool isGeneratingInitialBlocks=false;//Generando

	void Awake(){
		sharedInstance = this;
	}

	// Use this for initialization
	void Start () {
		GenerateInitialBlocks ();
	}

	public void GenerateInitialBlocks(){
		isGeneratingInitialBlocks = true;
		int i=0;
		for (i = 0; i < 3; i++) {
			AddNewBlock ();
		}
		isGeneratingInitialBlocks = false;
	}

	public void AddNewBlock(){
		//Seleccionar un bloque aleatorio entre los que tenemos disponibles
		int randomIndex = Random.Range(0,allTheLevelBlocks.Count);//Genera un numero aleatorio entre 0 y el ultimo numero

		if (isGeneratingInitialBlocks == true)
			randomIndex = 0;
		LevelBlock block = (LevelBlock)Instantiate (allTheLevelBlocks [randomIndex]);
		block.transform.SetParent (this.transform, false);//Hace que el padre sea el mismo

		//Posicion del bloque
		Vector3 blockPosition = Vector3.zero;

		if (currentLevelBlocks.Count == 0) {
			//Significa que es el primer bloque en colocarse
			blockPosition = levelInitialPoint.position;
		} else {
			//Ya tengo bloques en pantalla y lo empalmo al ultimo disponible
			blockPosition = currentLevelBlocks [currentLevelBlocks.Count - 1].exitPoint.position;
		}

		block.transform.position = blockPosition;
		currentLevelBlocks.Add (block);

	}

	public void RemoveOldBlock(){
		LevelBlock block = currentLevelBlocks [0];
		currentLevelBlocks.Remove (block);
		Destroy (block.gameObject);
	}

	public void RemoveAllBlocks(){
		while (currentLevelBlocks.Count > 0) {
			RemoveOldBlock();
		}
	}

}