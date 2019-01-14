using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System.Diagnostics;

public class EnvironnmentImpoter : MonoBehaviour
{
	public float TunnelPixelsPerUnit = 16;
	public float ObstaclePixelsPerUnit = 16;

	private static string EnvironnementPath = Application.streamingAssetsPath + "\\Environnements";


	public bool Import;

	void Awake()
	{
		for (int i = transform.childCount - 1; i >= 0; i--)
		{
			if (Application.isEditor)
				DestroyImmediate(transform.GetChild(i).gameObject);
			else
				Destroy(transform.GetChild(i).gameObject);
		}
		StartCoroutine(LoadAllEnvironnement().GetEnumerator());
	}
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.F2))
			Import = true;
		if (!Import)
			return;

		StartCoroutine(LoadAllEnvironnement().GetEnumerator());
	}

	IEnumerable LoadAllEnvironnement()
	{
		Import = false;
		File.WriteAllText(Application.streamingAssetsPath + "\\log.txt", "");

		Stopwatch watch = new Stopwatch();
		watch.Start();

		var managerEnvs = EnvironnementManager.Instance.Environnements;

		DirectoryInfo directoryInfo = new DirectoryInfo(EnvironnementPath);
		outprint("Streaming Assets Path: " + EnvironnementPath);
		outprint(directoryInfo.GetDirectories().Count() + " folders to parse");
		foreach (var environnementFolder in directoryInfo.GetDirectories())
		{
			var envName = environnementFolder.Name;
			Environnement environnement = null; ;
			if (managerEnvs.ContainsKey(envName))
			{
				outprint("Refreshing environnement : " + envName);
				environnement = transform.Find(envName).GetComponent<Environnement>();
			}
			else
			{
				var newGo = new GameObject(envName);
				newGo.transform.parent = this.transform;
				environnement = newGo.AddComponent<Environnement>();
			
				managerEnvs.Add(envName, environnement);
			}


			environnement.Obstacles = LoadSprites(environnementFolder + "\\Obstacles", ObstaclePixelsPerUnit);
			environnement.Tunnels = LoadSprites(environnementFolder + "\\Tunnels", TunnelPixelsPerUnit);


			watch.Stop();
			outprint("Importing Done in " + watch.ElapsedMilliseconds / 1000 + "s");
			yield return null;
		}
	}

	private static void outprint(string text)
	{
		File.AppendAllText(Application.streamingAssetsPath + "\\log.txt", text + "\n");
		UnityEngine.Debug.Log(text);
	}

	private List<Sprite> LoadSprites(string path, float pixelsPerUnits)
	{
		var sprites = new List<Sprite>();
		DirectoryInfo directoryInfo = new DirectoryInfo(path);
		var allSprites = directoryInfo.GetFiles("*.png")
		.Where(x => !x.Name.EndsWith(".meta"));
		outprint(allSprites.Count() + " Sprites to load");
		foreach (var item in allSprites)
			sprites.Add(LoadPNG(item.FullName, pixelsPerUnits));

		return sprites;
	}

	public static Sprite LoadPNG(string filePath, float pixelsPerUnits)
	{

		Texture2D tex = null;
		byte[] fileData;

		if (File.Exists(filePath))
		{
			fileData = File.ReadAllBytes(filePath);
			tex = new Texture2D(1, 1);
			tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		}
		else
		{
			outprint("doesnt exist " + filePath);
		}

		tex.filterMode = FilterMode.Point;
		var sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), pixelsPerUnits);
		return sprite;
	}

}
