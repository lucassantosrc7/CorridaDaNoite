using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Voltas : MonoBehaviour {

	public GameObject player;
	public int numerodeVoltas;
	public int numerodePontos;
	private int conta;
	private int cont_volta;
	public int passou = 0;
	public static bool perdeu = false;
	public GameObject textoVitoria;
	public GameObject textoDerrota;
	public GameObject HUDFim;
	public GameObject HUD;
	public Text volta;

	void Start () {
		conta = numerodePontos * numerodeVoltas;
		textoDerrota.SetActive (false);
		textoVitoria.SetActive (false);
	}

	void Update () {

		/*if (Input.GetKeyDown (KeyCode.V)) {
			passou = numerodePontos * numerodeVoltas +1;
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			perdeu = true;
		}*/

		if (cont_volta == numerodePontos) {
			volta.text = 1.ToString ("0") + "/ " + 2.ToString("0");
		}else if (cont_volta == numerodePontos * 2) {
			volta.text = 2.ToString ("0") + "/ " + 2.ToString("0");
		}

		if (passou > conta) {
			textoVitoria.SetActive (true);
			player.GetComponent<Move> ().enabled = false;
			StartCoroutine (acabar ());
		} else if (perdeu) {
			textoDerrota.SetActive (true);
			player.GetComponent<Move> ().enabled = false;
			StartCoroutine (acabar ());
		}
	}
	IEnumerator acabar(){
		yield return new WaitForSeconds (1);
		HUDFim.SetActive (true);
		HUD.SetActive (false);
		Time.timeScale = 0;
	}
}
