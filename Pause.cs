using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

	public GameObject menu_Pausa;
	public GameObject HUD;
	public GameObject sair;

	public AudioClip somClick;
	private AudioSource source;

	void Start(){
		source = GetComponent<AudioSource> ();
		sair.SetActive (false);
	}
	void Update(){
		if (Input.GetKey (KeyCode.Escape)) {
			HUD.SetActive (false);
			menu_Pausa.SetActive (true);
			Time.timeScale = 0;
			source.PlayOneShot (somClick);
		}
	}

	public void Jogar(){
		HUD.SetActive (true);
		menu_Pausa.SetActive (false);
		Time.timeScale = 1;
		source.PlayOneShot (somClick);
	} 
	public void menu(){
		SceneManager.LoadScene ("Menu");
		Time.timeScale = 1;
		Menu.menu_bool = true;
		source.PlayOneShot (somClick);
	}
	public void menuPista(){
		SceneManager.LoadScene ("Menu");
		Menu.menu_bool = false;
		Time.timeScale = 1;
		source.PlayOneShot (somClick);
	}
		

	public void Sair(){
		sair.SetActive (true);
		source.PlayOneShot (somClick);
	}
	public void Sim(){
		Application.Quit ();
		source.PlayOneShot (somClick);
	}
	public void Nao(){
		sair.SetActive (false);
		source.PlayOneShot (somClick);
	}
}
