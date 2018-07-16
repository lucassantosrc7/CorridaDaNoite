using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinamapaMove : MonoBehaviour {

    public GameObject Player;

	void Start () {
		
	}
	
	void Update () {
        transform.position = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
        transform.rotation = Player.transform.rotation;
	}
}
