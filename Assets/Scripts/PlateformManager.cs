using System;
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

	public int OrderInLayer = 10000;

	public float SlowmoStartDistance = 10;

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
			SpawnAPlateformePlease();
		}

		Slowmoooo();
	}

	private void Slowmoooo()
	{
		var plateforms = (Plateform[])GameObject.FindObjectsOfType(typeof(Plateform));
		Time.timeScale = 1f;
		if (plateforms.Count() != 0)
		{
			var topPlateform = plateforms.Last();
			if (topPlateform.transform.position.z < SlowmoStartDistance)
			{
				float t = 1 - topPlateform.transform.position.z / SlowmoStartDistance;
				Time.timeScale = 0.5f + EaseFunctions.SmoothStart3(t)/0.5f;
				Debug.Log("t :" + (0.5f + EaseFunctions.SmoothStart3(t)/0.5f));
			}

		}
	}

	private void SpawnAPlateformePlease()
	{
		var p = Plateforms[UnityEngine.Random.Range(0, Plateforms.Count())];

		var renders = p.GetComponentsInChildren<SpriteRenderer>();
		foreach (var r in renders)
			r.sortingOrder = OrderInLayer--;

		Instantiate(p, new Vector3(0, 0, SpawnDistance), Quaternion.identity);
	}
}
