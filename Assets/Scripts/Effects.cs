using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Effects
{

	public static IEnumerator LerpColor(Action<Color> ToColor, Color from, Color to, float duration)
	{
		float t = 0;
		while (t < duration)
		{
			t += Time.deltaTime;
			float tt = t / duration;
			float ttt = tt * tt * (3f - 2f * tt);
			ToColor(Color.Lerp(from, to, ttt));
			yield return null;
		}
		ToColor(to);
	}

}
