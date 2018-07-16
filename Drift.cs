using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drift : MonoBehaviour {

    private float carroAnguloY;
    private Rigidbody rb;

    //Acerleração Drift
    public float MaxDrift;
    public float DesDrift;
    public float driftAcc;
    private float drift_Vel;
    private int drift_Axis;

	//Rotacao
	[Space(20)]
	public float Max_rotacao;
	public float rotacaoAcc;
	private float rotacao;

	//Desaceleração
	[Space(20)]
	public float des_Carro;
	private float Vel_carro;

    void Start () {
        rb = GetComponent<Rigidbody>();
	}

	void Update () {

		if (Input.GetMouseButtonDown (1)) {
			Vel_carro = Move.velocidadeSpeed;
		}
        if (Input.GetMouseButton(1))
        {
            drift();
        }
		else if(Input.GetMouseButtonUp(1)) {
			Move.velocidadeSpeed += drift_Vel;
			drift_Axis = 0;
			drift_Vel = 0;
        }
	}

	void drift(){
		//print(Move.velocidadeSpeed);
        transform.Translate(Vector3.right * Time.deltaTime * drift_Vel);

		if (Input.GetKey (KeyCode.D) && Vel_carro >= 0.5f) {
			transform.Rotate (Vector3.up * (Move.rotacaoSpeed + rotacao) * Time.deltaTime);
			drift_Axis = -1;
			Aceleracao ();
		} else if (Input.GetKey (KeyCode.A) && Vel_carro >= 0.5f) {
			transform.Rotate (Vector3.down * (Move.rotacaoSpeed + rotacao) * Time.deltaTime);
			drift_Axis = 1;
			Aceleracao ();
		} else if (Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.D)) {
			Move.velocidadeSpeed = drift_Vel;
		}
        else
        {
			if (Move.velocidadeSpeed > 0) { Move.velocidadeSpeed -= 0.5f; }
			else if (Move.velocidadeSpeed <= 0) { Move.velocidadeSpeed = 0; }
		}
	}
    void Aceleracao() {
        if (drift_Axis == -1 && drift_Vel >= -MaxDrift) {
            drift_Vel -= driftAcc;
        }else if (drift_Axis == 1 && drift_Vel <= MaxDrift){
            drift_Vel += driftAcc;
        }

		//Rotacao
		if (rotacao <= Max_rotacao) {
			rotacao += rotacaoAcc;
		}

		//Desaceleracao
		if (Move.velocidadeSpeed > 2) {
			Move.velocidadeSpeed -= des_Carro;
		} else {
			Move.velocidadeSpeed = 1;
		}

    }
}
