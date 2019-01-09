using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kwame : MonoBehaviour
{

	public SpriteRenderer SR;
	public Plateform CurrentPlateform;


	void Update()
	{

		var plateformPX = CurrentPlateform.Sprite.sprite.texture.GetPixels();
		var plateformPPU = CurrentPlateform.Sprite.sprite.pixelsPerUnit;



		var txt = new Texture2D(128, 128, TextureFormat.ARGB32, false);
		txt.filterMode = FilterMode.Point;
		txt.wrapMode = TextureWrapMode.Clamp;

		for (int x = 0; x < 128; x++)
		{
			for (int y = 0; y < 128; y++)
			{
				txt.SetPixel(x, y, new Color(0, 0, 0, 0));

			}
		}

		foreach (var p in (Player[])GameObject.FindObjectsOfType(typeof(Player)))
		{
			var pp = p.Sprite.sprite.texture.GetPixels();
			var playerPPU = p.Sprite.sprite.pixelsPerUnit;
			var pRect = p.Sprite.sprite.textureRect;

			int px = (int)((p.transform.position.x + 7.3) * playerPPU);
			int py = (int)((p.transform.position.y + 7.3) * playerPPU);



			float ppuRatio = plateformPPU / playerPPU;

			for (int x = 0; x < pRect.width; x++)
			{
				for (int y = 0; y < pRect.height; y++)
				{
					if (pp[(int)(y * pRect.width + x)].a != 0)
					{
						int cx = (int)((px + x) * ppuRatio);
						int cy = (int)((py + y) * ppuRatio);
						int index = cy * 128 + cx;
						if (index < 0 || index >= 128 * 128 || cx >= 128 || cx < 0 || cy < 0){

						}else if (plateformPX[cy * 128 + cx].a == 0)
							txt.SetPixel(cx, cy, Color.green);
						else
							txt.SetPixel(cx, cy, Color.red);
					}
				}
			}
		}

		txt.Apply();
		Sprite s = Sprite.Create(txt, new Rect(0.0f, 0.0f, 128, 128), new Vector2(0.5f, 0.5f), 8);
		SR.sprite = s;

	}
}
