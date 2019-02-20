using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode()]
public class LevelManager : Singleton<LevelManager>
{

	public List<Level> Levels = new List<Level>();

	private int _currentLevelIndex = 0;
	public int CurrentLevelIndex
	{
		set
		{
			_currentLevelIndex = value;
			OnCurrentLevelChanged?.Invoke(Levels[CurrentLevelIndex]);
		}
		get
		{
			return _currentLevelIndex;
		}
	}

	public Action<Level> OnCurrentLevelChanged;

	public Sprite NextTunnel
	{
		get
		{
			var env = Levels[CurrentLevelIndex];
			return env.Tunnels[UnityEngine.Random.Range(0, env.Tunnels.Count)];
		}
	}

	public Sprite NextObstacle
	{
		get
		{
			var env = Levels[CurrentLevelIndex];
			return env.Obstacles[UnityEngine.Random.Range(0, env.Obstacles.Count)];
		}
	}

	protected override void Awake()
	{
		base.Awake();

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F6))
		{
			CurrentLevelIndex = (CurrentLevelIndex + 1) % Levels.Count;
		}
	}
}
