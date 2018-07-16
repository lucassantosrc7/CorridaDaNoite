
using UnityEngine;


public class PathFindingWayPoints : MonoBehaviour {

	public float 		Max_Vel;
	public float 		acc;
	private  float      speed;
	private  float      Finalspeed;
	public  float       rotSpeed;
	public float Habilidade; //Quanto maior pior vai ser
	[HideInInspector]public int num_Voltas;
	public GameObject PontodeChegada;
	public  Transform[] waypoints;
	private int         currentWayPoint;
	private Rigidbody   rb;

	private float [] sorteio;

	public void Start() {
		num_Voltas = PontodeChegada.GetComponent<Voltas>().numerodeVoltas;
		currentWayPoint = 0;
		rb              = GetComponent<Rigidbody>();
		Finalspeed = speed;
	}

	public void FixedUpdate() {
		if (Move.tempo <= -1) {
			if (num_Voltas <= 0) {
				Voltas.perdeu = true;
			}

			//Acelereção
			if(speed < Max_Vel){
				speed += acc;
			}

			if (Move.especial) {
				Finalspeed = speed / 10;
			} else {
				Finalspeed = speed;
			}



			Vector3 dir = waypoints [currentWayPoint].position - transform.position;
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dir), Time.deltaTime * rotSpeed);
			transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);

			if (dir.sqrMagnitude <= 1) {
				currentWayPoint++;
				if (currentWayPoint >= waypoints.Length) {
					currentWayPoint = 0;
					num_Voltas--;
				}
				if (!waypoints [currentWayPoint].GetComponent<WaypointControl> ().previous) {
					float variacao = waypoints [currentWayPoint].GetComponent<WaypointControl> ().variacao + Habilidade;
					sorteio = new float[]{ variacao, -variacao };
					variacao = sorteio [Random.Range (0, sorteio.Length)];
					waypoints [currentWayPoint].GetComponent<WaypointControl> ().variacao = variacao;
					waypoints [currentWayPoint].position = new Vector3 (waypoints [currentWayPoint].position.x, 0.5f,
						waypoints [currentWayPoint].position.z + variacao);
				} else {
					waypoints [currentWayPoint].position = new Vector3 (waypoints [currentWayPoint].position.x, 0.5f,
						waypoints [currentWayPoint].position.z + waypoints [currentWayPoint].GetComponent<WaypointControl> ().variacao);
				}
			} else {
				rb.MovePosition (transform.position + transform.forward * Time.deltaTime * Finalspeed);
			}
		}
	}
}
