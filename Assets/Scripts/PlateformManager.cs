﻿using System.Linq;

public class PlateformManager : Singleton<PlateformManager>
{


	public float PlateformMoveSpeed;
	public Player[] Players;

	public Plateform[] Plateforms;

	public Player[] AlivePlayer => Players.Where(p => p.State.Equals(Player.States.Alive)).ToArray();

	public float TimeBetweenPlatform;
	private float t;
	public float SpawnDistance;

	public int OrderInLayer = 10000;

	//public List<LevelElement
	// Use this for initialization
	void Start() => Players = FindObjectsOfType<Player>();

	// Update is called once per frame; WHAAATTT!??! every frame!!?
	//void Update()
	//{
	//	t -= Time.deltaTime;
	//	if (t <= 0)
	//	{
	//		t += TimeBetweenPlatform;
	//		var p = Plateforms[Random.Range(0, Plateforms.Count())];

	//		var renders = p.GetComponentsInChildren<SpriteRenderer>();
	//		foreach (var r in renders)
	//			r.sortingOrder = OrderInLayer--;

	//		Instantiate(p, new Vector3(0, 0, SpawnDistance), Quaternion.identity);
	//	}
	//}
}
