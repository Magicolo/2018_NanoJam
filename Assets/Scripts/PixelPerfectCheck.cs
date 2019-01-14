using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PixelPerfectCheck
{

	public static bool Collides(Plateform currentPlateform, Player player, int nbPixelToCollide)
	{
		var plateformPX = currentPlateform.Sprite.sprite.texture.GetPixels();
		var plateformPPU = currentPlateform.Sprite.sprite.pixelsPerUnit;

		var pp = player.Sprite.sprite.texture.GetPixels();
		var playerPPU = player.Sprite.sprite.pixelsPerUnit;
		var pRect = player.Sprite.sprite.textureRect;

		int px = (int)((player.transform.position.x + 12) * playerPPU);
		int py = (int)((player.transform.position.y + 12) * playerPPU);


		float ppuRatio = plateformPPU / playerPPU;
		int nbPixelCollided = 0;

		for (int x = 0; x < pRect.width; x++)
		{
			for (int y = 0; y < pRect.height; y++)
			{
				if (pp[(int)(y * pRect.width + x)].a != 0)
				{
					int cx = (int)((px + x) * ppuRatio);
					int cy = (int)((py + y) * ppuRatio);
					int index = cy * 128 + cx;
					if (index < 0 || index >= 128 * 128 || cx >= 128 || cx < 0 || cy < 0)
					{

					}
					else if (plateformPX[cy * 128 + cx].a == 0)
					{
						//txt.SetPixel(cx, cy, Color.green);
					}

					else
					{
						nbPixelCollided++;
						//Debug.Log(nbPixelCollided);
						if (nbPixelCollided >= nbPixelToCollide)
						{
							return true;
						}
					}
					//txt.SetPixel(cx, cy, Color.red);

				}
			}
		}
		return false;

	}
}
