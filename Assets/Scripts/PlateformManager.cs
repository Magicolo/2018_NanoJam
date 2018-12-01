using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateformManager : Singleton<PlateformManager>
{


	public float PlateformMoveSpeed;
	public Player[] Players;

	public Plateform[] Plateforms;

	public Player[] AlivePlayer { get { return Players.Where(p => p.State.Equals(Player.States.Alive)).ToArray(); } }

	public float TimeBetweenPlatform;
	private float t;
	public float SpawnDistance;

	//public List<LevelElement
	// Use this for initialization
	void Start()
	{
		Players = FindObjectsOfType<Player>();
	}

	// Update is called once per frame
	void Update()
	{
		t -= Time.deltaTime;
		if (t <= 0)
		{
			t += TimeBetweenPlatform;
			var p = Plateforms[Random.Range(0, Plateforms.Count())];
			Instantiate(p, new Vector3(0, 0, SpawnDistance), Quaternion.identity);
		}
	}
}
