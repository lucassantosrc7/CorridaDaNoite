using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PontosDaCorrida : MonoBehaviour {

	public bool passou = false;
	public GameObject Ponto_Anterior;
	public GameObject Ponto_de_Chegada;

	void Start () {
		
	}

	void Update () {
		
	}

	void OnTriggerEnter(Collider hit){
		if (hit.CompareTag ("Player") && Ponto_Anterior.GetComponent<PontosDaCorrida> ().passou) {
			passou = true;
			Ponto_Anterior.GetComponent<PontosDaCorrida> ().passou = false;
			Ponto_de_Chegada.GetComponent<Voltas> ().passou ++;
		}
	}
}
