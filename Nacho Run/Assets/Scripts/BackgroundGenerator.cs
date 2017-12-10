using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGenerator : MonoBehaviour {

	public static BackgroundGenerator sharedInstance; //Instancia compartida para solo tener un generador de escenarios
	public List<BackgroundBlock> allTheBackgroundBlocks = new List<BackgroundBlock>();//Lista que contiene todos los niveles que se han creado
	public List<BackgroundBlock> currentBackgroundBlocks = new List<BackgroundBlock>();//Lista de los bloques que tenemos en pantalla
	public Transform backgroundInitialPoint;//Punto inicial donde empezara a crearse el primer nivel de todos
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
		int randomIndex = Random.Range(0,allTheBackgroundBlocks.Count);//Genera un numero aleatorio entre 0 y el ultimo numero

		if (isGeneratingInitialBlocks == true)
			randomIndex = 0;
		BackgroundBlock block = (BackgroundBlock)Instantiate (allTheBackgroundBlocks [randomIndex]);
		block.transform.SetParent (this.transform, false);//Hace que el padre sea el mismo

		//Posicion del bloque
		Vector3 blockPosition = Vector3.zero;

		if (currentBackgroundBlocks.Count == 0) {
			//Significa que es el primer bloque en colocarse
			blockPosition = backgroundInitialPoint.position;
		} else {
			//Ya tengo bloques en pantalla y lo empalmo al ultimo disponible
			blockPosition = currentBackgroundBlocks [currentBackgroundBlocks.Count - 1].exitPoint.position;
		}

		block.transform.position = blockPosition;
		currentBackgroundBlocks.Add (block);

	}

	public void RemoveOldBlock(){
		BackgroundBlock block = currentBackgroundBlocks [0];
		currentBackgroundBlocks.Remove (block);
		Destroy (block.gameObject);
	}

	public void RemoveAllBlocks(){
		while (currentBackgroundBlocks.Count > 0) {
			RemoveOldBlock();
		}
	}

}