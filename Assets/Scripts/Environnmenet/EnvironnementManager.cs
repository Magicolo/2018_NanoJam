using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode()]
public class EnvironnementManager : Singleton<EnvironnementManager>
{

	public Dictionary<string, Environnement> Environnements = new Dictionary<string, Environnement>();

	public Sprite NextTunnel
	{
		get
		{
			var env = Environnements.First().Value;
			return env.Tunnels[UnityEngine.Random.Range(0, env.Tunnels.Count)];
		}
	}

	public Sprite NextObstacle
	{
		get
		{
			var env = Environnements.First().Value;
			return env.Obstacles[UnityEngine.Random.Range(0, env.Obstacles.Count)];
		}
	}

	protected override void Awake()
	{
		base.Awake();
		/* foreach (var d in Directory.GetDirectories(@"Assets\Resources\Sprite\Environnement"))
		{
			var splitted = d.Split('\\');
			var resourceFolder = string.Join("\\", splitted.Skip(2).ToArray());
			var environnementName = d.Split('\\').Last();
			var rs = Resources.LoadAll(resourceFolder);
			var sprites = rs.Where(x => x.GetType() == typeof(Sprite)).Select(x => (Sprite)x).ToList();
			//Debug.Log(environnementName + " - " + sprites.Count);
			//Environnements.Add(environnementName, sprites);
		} */
	}

	// Update is called once per frame
	void Update()
	{

	}
}
