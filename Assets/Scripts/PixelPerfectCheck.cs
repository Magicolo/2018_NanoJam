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

		int px = (int)((player.transform.position.x + 7.3) * playerPPU);
		int py = (int)((player.transform.position.y + 7.3) * playerPPU);


		float ppuRatio = plateformPPU / playerPPU;
		int nbPixelCollided = 0;

		for (int x = 0; x < pRect.width; x++)
		{
			for (int y = 0; y < pRect.height; y++)
			{
				if (pp[(int)(y * pRect.width + x)].a != 0)
				{
					nbPixelCollided++;
					if (nbPixelCollided >= nbPixelToCollide)
					{
						return true;
					}
				}
			}
		}
		return false;

	}
}
