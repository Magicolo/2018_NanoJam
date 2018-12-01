using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateformManager : Singleton<PlateformManager> {


	public float PlateformMoveSpeed;
	public Player[] Players;

	//public List<LevelElement
	// Use this for initialization
	void Start () {
		Players = FindObjectsOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
