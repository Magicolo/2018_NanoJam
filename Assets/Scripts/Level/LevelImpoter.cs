using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System.Diagnostics;

public class LevelImpoter : MonoBehaviour
{
	public float TunnelPixelsPerUnit = 16;
	public float ObstaclePixelsPerUnit = 16;

	private static string LevelPath = Application.streamingAssetsPath + "\\Levels";


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
		StartCoroutine(LoadAllLevels().GetEnumerator());
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

		StartCoroutine(LoadAllLevels().GetEnumerator());
	}

	IEnumerable LoadAllLevels()
	{
		Import = false;
		File.WriteAllText(Application.streamingAssetsPath + "\\log.txt", "");

		Stopwatch watch = new Stopwatch();
		watch.Start();

		var levelDictionary = LevelManager.Instance.Levels;

		DirectoryInfo directoryInfo = new DirectoryInfo(LevelPath);
		outprint("Streaming Assets Path: " + LevelPath);
		outprint(directoryInfo.GetDirectories().Count() + " folders to parse");
		foreach (var levelFolder in directoryInfo.GetDirectories())
		{
			var envName = levelFolder.Name;
			Level level = null; ;
			if (levelDictionary.Any(e => e.name == envName))
			{
				outprint("Refreshing level : " + envName);
				level = transform.Find(envName).GetComponent<Level>();
			}
			else
			{
				outprint("New level : " + envName);
				var newGo = new GameObject(envName);
				newGo.transform.parent = this.transform;
				level = newGo.AddComponent<Level>();

				levelDictionary.Add(level);
			}


			level.Obstacles = LoadSprites(levelFolder + "\\Obstacles", ObstaclePixelsPerUnit);
			level.Tunnels = LoadSprites(levelFolder + "\\Tunnels", TunnelPixelsPerUnit);


			watch.Stop();
			outprint("Importing Done in " + watch.ElapsedMilliseconds / 1000 + "s");
			yield return null;
		}
	}

	private static void outprint(string text)
	{
		File.AppendAllText(Application.streamingAssetsPath + "\\log.txt", text + "\n");
		//UnityEngine.Debug.Log(text);
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
