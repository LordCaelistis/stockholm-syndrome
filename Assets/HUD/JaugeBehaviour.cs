using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JaugeBehaviour : MonoBehaviour {

	//
	public GameObject contenuJauge;
	public Timer gameMaster;
	public int player;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		contenuJauge.transform.localScale = new Vector3(
			1f,
			Mathf.Clamp(
				gameMaster.playerScore[player-1]/gameMaster.winningScore,
				0f,
				1f
			),
			1f
		);
	}
}
