using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	#region Menu
	public GameObject menu;
	public GameObject menu_Pistas;
	public GameObject creditos;
	public GameObject temCTZ;
	public static bool menu_bool = true;
	#endregion

	private AudioSource source;
	public AudioClip somClick;

	void Start(){
		source = GetComponent<AudioSource> ();
		if (menu_bool) {
			menu.SetActive (true);
			menu_Pistas.SetActive (false);
		} else {
			menu_Pistas.SetActive (true);
			menu.SetActive (false);
		}
	}

	#region MenuFuncao
	public void Jogar(){
		menu_Pistas.SetActive (true);
		menu.SetActive (false);
	}
	public void Sair(){
		temCTZ.SetActive (true);
		source.PlayOneShot (somClick);
	}
	public void Creditos(){
		creditos.SetActive (true);
		menu.SetActive (false);
		source.PlayOneShot (somClick);
	}
	public void voltar(){
		menu_Pistas.SetActive (false);
		creditos.SetActive (false);
		menu.SetActive (true);
	}
	#endregion

	#region MenssagemFuncao
	public void Sim(){
		Application.Quit ();
		source.PlayOneShot (somClick);
	}
	public void Nao(){
		temCTZ.SetActive (false);
		source.PlayOneShot (somClick);
	}
	#endregion
}


