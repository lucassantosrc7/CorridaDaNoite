using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chover : MonoBehaviour {

	public GameObject Pista;
	public GameObject PistaChuva;
	private Toggle toggle;

	void Start () {
		toggle = GetComponent<Toggle> ();
	}
	

	void Update () {
		if (toggle.isOn == false) {
			Pista.SetActive (true);
			PistaChuva.SetActive (false);
		} else if (toggle.isOn == true) {
			Pista.SetActive (false);
			PistaChuva.SetActive (true);
		}
	}
}
