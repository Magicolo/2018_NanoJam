using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

[ExecuteInEditMode()]
public class TestImporter : MonoBehaviour
{
	public SpriteRenderer sr;

	public bool Import;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (!Import)
			return;

		StartCoroutine(something().GetEnumerator());
	}

	IEnumerable something()
	{
		Import = false;
		DirectoryInfo directoryInfo = new DirectoryInfo(Application.streamingAssetsPath);
		print("Streaming Assets Path: " + Application.streamingAssetsPath);
		FileInfo[] allFiles = directoryInfo.GetFiles("*.*");
		foreach (var item in allFiles)
		{
			print(item);
		}

		string wwwPlayerFilePath = "file://" + Application.streamingAssetsPath + "//Tileset_16.png";

		using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(wwwPlayerFilePath))
		{
			yield return uwr.SendWebRequest();

			if (uwr.isNetworkError || uwr.isHttpError)
			{
				Debug.Log(uwr.error);
			}
			else
			{
				var texture = DownloadHandlerTexture.GetContent(uwr);
				float ppu = 10;
				texture.filterMode = FilterMode.Point;
				sr.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), ppu);
			}
		}

	}
}
