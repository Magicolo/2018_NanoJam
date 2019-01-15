using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using System.Diagnostics;

public abstract class ImporterBase<T> : MonoBehaviour where T : MonoBehaviour
{
	protected abstract string AssetTypeName { get; }
	protected abstract string Path { get; }
	protected abstract KeyCode ForceReloadKey { get; }
	protected abstract List<T> CurrentAssets { get; }


	public bool Import;


	protected virtual void Awake()
	{
		for (int i = transform.childCount - 1; i >= 0; i--)
		{
			if (Application.isEditor)
				DestroyImmediate(transform.GetChild(i).gameObject);
			else
				Destroy(transform.GetChild(i).gameObject);
		}
		StartCoroutine(LoadAllAssets().GetEnumerator());
	}

	void Update()
	{
		if (Input.GetKeyDown(ForceReloadKey))
			Import = true;
		if (!Import)
			return;

		StartCoroutine(LoadAllAssets().GetEnumerator());
	}

	IEnumerable LoadAllAssets()
	{
		Import = false;
		File.WriteAllText(Application.streamingAssetsPath + "\\log.txt", "");

		Stopwatch watch = new Stopwatch();
		watch.Start();


		DirectoryInfo directoryInfo = new DirectoryInfo(Path);
		outprint("Streaming Assets Path: " + Path);
		outprint(directoryInfo.GetDirectories().Count() + " folders to parse");
		foreach (var assetFolder in directoryInfo.GetDirectories())
		{
			var assetName = assetFolder.Name;
			T assetObject = null;
			if (CurrentAssets.Any(e => e.name == assetName))
			{
				outprint($"Refreshing {AssetTypeName} : {assetName}");
				assetObject = transform.Find(assetName).GetComponent<T>();
			}
			else
			{
				outprint("New level : " + assetName);
				var newGo = new GameObject(assetName);
				newGo.transform.parent = this.transform;
				assetObject = newGo.AddComponent<T>();
			}

			LoadAsset(assetObject,assetFolder);


			watch.Stop();
			outprint("Importing Done in " + watch.ElapsedMilliseconds / 1000 + "s");
			yield return null;
		}
	}

	protected abstract void LoadAsset(T assetObject, DirectoryInfo assetFolder);

	private static void outprint(string text)
	{
		File.AppendAllText(Application.streamingAssetsPath + "\\log.txt", text + "\n");
		//UnityEngine.Debug.Log(text);
	}

	protected List<Sprite> LoadSprites(string path, float pixelsPerUnits)
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
