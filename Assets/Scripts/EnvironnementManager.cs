using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode()]
public class EnvironnementManager : MonoBehaviour
{

	public Dictionary<string, List<Sprite>> Environnements = new Dictionary<string, List<Sprite>>();

	void Awake()
	{
		foreach (var d in Directory.GetDirectories(@"Assets\Resources\Sprite\Environnement"))
		{
			var splitted = d.Split('\\');
			var resourceFolder = string.Join("\\", splitted.Skip(2).ToArray());
			var environnementName = d.Split('\\').Last();
			var rs = Resources.LoadAll(resourceFolder);
			var sprites = rs.Where(x=>x.GetType() == typeof(Sprite)).Select(x=>(Sprite)x).ToList();
			//Debug.Log(environnementName + " - " + sprites.Count);
			Environnements.Add(environnementName, sprites);
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
