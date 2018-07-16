using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour {

	private enum Estado
	{
		ColidiuRe, ColidiuFrente, NãoColidiu
	}
	private Estado estado;

	private Rigidbody rb;
	private Vector3 a;

	private float vida;

	public float aceleracao;
	private float acc;

	public float desaceleracao;
	public float desaceleracaoR;
	public float freio;
	public static float velocidadeSpeed;
	public float FinalSpeed;
	private float velocidadeFinal;

	private float rotacaoAcc;
	public static float rotacaoSpeed;
	public float rotacaoFinal;

	private bool C = true;
	private bool B = true;

	private float driftAcc = 0;
	private float carroAnguloY;

	public float tempo_Especial;
	public static bool especial = false;
	public int Num_Especial = 3;
	public ParticleSystem chuva;
	private ParticleSystem.MainModule chuva_main;

	public float tempo_Nitro;
	public int Num_Nitro = 3;
	private bool nitro = false;

	//HUD
	[Space(20)]
	public GameObject[] B_Nitro;
	public GameObject[] B_Especial;
	public Text texto;
	private float velocidade;

	public GameObject[] fumaca;
	public GameObject textoDerrota;

	public Text cronometro;
	public static int tempo = 3;
	private float largada_Tempo = 1;

	void Start () {
		rb = GetComponent<Rigidbody> ();
		vida = FinalSpeed * 3;
		fumaca [0].SetActive (false);
		fumaca [1].SetActive (false);
		fumaca [2].SetActive (false);
		chuva_main = chuva.main;
		cronometro.text = tempo.ToString ("0");
	}

	void Update () {

		/*if (Input.GetKey (KeyCode.DownArrow)) {
			vida--;
		}*/
		if (tempo <= -1) {
			cronometro.gameObject.SetActive (false);

			if (Input.GetMouseButtonDown (0) && !nitro && Num_Nitro > 0) {
				nitro = true;
				Num_Nitro--;
				StartCoroutine (Nitro ());
			}

			if (Input.GetKeyDown (KeyCode.Space) && !especial && Num_Especial > 0) {
				especial = true;
				Num_Especial--;
				StartCoroutine (Especial ());
			}

			if (transform.eulerAngles.z != 0)
				transform.eulerAngles = new Vector3 (transform.eulerAngles.x, transform.eulerAngles.y, 0); //Apenas pro carro não sair do eixo Z

			if (Input.GetKey (KeyCode.W) && velocidadeSpeed <= FinalSpeed && estado != Estado.ColidiuFrente) {
				C = true;
				velocidadeSpeed += acc;
				estado = Estado.NãoColidiu;
			} else
				C = false;

			if (Input.GetKey (KeyCode.S) && estado != Estado.ColidiuRe) {
				B = true;
				velocidadeSpeed -= freio;
				estado = Estado.NãoColidiu;
			} else
				B = false;

			if (Input.GetKey (KeyCode.D) && (velocidadeSpeed <= -0.5 || velocidadeSpeed >= 0.5) && !Input.GetMouseButton (1)) {
				transform.Rotate (Vector3.up * rotacaoSpeed  * Time.deltaTime);
			}
			if (Input.GetKey (KeyCode.A) && (velocidadeSpeed <= -0.5 || velocidadeSpeed >= 0.5) && !Input.GetMouseButton (1)) {
				transform.Rotate (Vector3.down * rotacaoSpeed  * Time.deltaTime);
			}
		} else if(Time.time > largada_Tempo) {
			tempo--;
			largada_Tempo = Time.time + 1;
			if (tempo >= 1) {
				cronometro.text = tempo.ToString ("0");
			} else {
				cronometro.text = "Vai";
				largada_Tempo = Time.time + 0.1f;
			}
		}
	
    }

	void FixedUpdate(){

		rb.MovePosition (transform.position + transform.forward * Time.deltaTime * velocidadeFinal);

		Aceleracao ();
		Desaceleracao ();

		if (especial) {
			velocidadeFinal = velocidadeSpeed / 10;
			chuva_main.startSpeed = 5;
			if (nitro) {
				velocidadeFinal = velocidadeSpeed * 2.5f;
			}
		} else if (nitro) {
			velocidadeFinal = velocidadeSpeed * 2.5f;
		} else if (!nitro && velocidadeFinal > velocidadeSpeed) {
			velocidadeFinal-= 1;
		} else {
			velocidadeFinal = velocidadeSpeed;
			rotacaoSpeed = rotacaoAcc;
		}

		////////H U D////////
		if(vida <= FinalSpeed * 2 && vida > FinalSpeed){
			fumaca [0].SetActive (true);
		}else if(vida <= FinalSpeed && vida > 0){
			fumaca [1].SetActive (true);
		}else if(vida <= 0){
			fumaca [2].SetActive (true);
			textoDerrota.SetActive (true);
			Voltas.perdeu = true;
			GetComponent<Move> ().enabled = false;
		}

		if(Num_Nitro == 2){
			B_Nitro [2].SetActive (false);
		}else if(Num_Nitro == 1){
			B_Nitro [1].SetActive (false);
		}else if(Num_Nitro <= 0){
			B_Nitro [0].SetActive (false);
		}

		if(Num_Especial == 2){
			B_Especial [2].SetActive (false);
		}else if(Num_Especial == 1){
			B_Especial [1].SetActive (false);
		}else if(Num_Especial <= 0){
			B_Especial [0].SetActive (false);
		}

		if (especial) {
			velocidade = ((velocidadeFinal * 250) / FinalSpeed) * 10;
		} else if (velocidadeFinal > FinalSpeed - 2 && velocidadeFinal < FinalSpeed + 2) {
			velocidade = 250;
		} else if (velocidadeFinal >= 0) {
			velocidade = (velocidadeFinal * 250) / FinalSpeed;
		} else {
			velocidade = ((velocidadeFinal * 250) / FinalSpeed)*-1;
		}
		texto.text = velocidade.ToString ("0");

	}

	void Desaceleracao(){
		if (!C && !B && velocidadeSpeed > 0)
			velocidadeSpeed -= desaceleracao;
		else if (!C && !B && velocidadeSpeed <= 0)
			velocidadeSpeed = 0;
	}

	void Aceleracao(){
		if (velocidadeSpeed < 10) {
			acc = aceleracao/4;
			rotacaoAcc = rotacaoFinal * 0.25f;
		}
		else if (velocidadeSpeed < 20) {
			acc = aceleracao/3;
			rotacaoAcc = rotacaoFinal *  0.5f;
		}
		else if (velocidadeSpeed < 40) {
			acc = aceleracao/2;
			rotacaoAcc = rotacaoFinal *  0.75f;
		}
		else if (velocidadeSpeed >= 40) {
			acc = aceleracao;
			rotacaoAcc = rotacaoFinal *  1;
		}
	}

	void OnCollisionEnter(Collision hit){
		
		if (hit.gameObject.CompareTag ("Parede")) {
			if (velocidadeFinal > 0) {
				estado = Estado.ColidiuFrente;
				vida -= velocidadeFinal;
			} else if (velocidadeFinal < 0) {
				estado = Estado.ColidiuRe;
				vida -= freio;
			}
			rb.MovePosition (transform.position + transform.forward * Time.deltaTime * -velocidadeFinal);
			velocidadeSpeed = 0;
		}
	}

	IEnumerator Especial(){
		yield return new WaitForSeconds (tempo_Especial);
		especial = false;
		chuva_main.startSpeed = 50;
	}
	IEnumerator Nitro(){
		yield return new WaitForSeconds (tempo_Nitro);
		nitro = false;
	}


}
