using System.Linq;
using UnityEngine;

public class SlowmoManager : Singleton<SlowmoManager>
{

	public float SlowmoStartDistance;
	public float MaxDistanceDistance;
	public float SlowExponent = 0.75f;
	public float SlowRatio = 0.25f;

	void Update()
	{
		var plateforms = (Plateform[])GameObject.FindObjectsOfType(typeof(Plateform));
		if (plateforms.Count() != 0)
		{
			var first = plateforms.First();
			var z = first.transform.position.z;
			if (z < SlowmoStartDistance && z > MaxDistanceDistance)
			{
				float t = 0;
				if (z > 0)
					t = z / SlowmoStartDistance;
				else
					t = z / MaxDistanceDistance;

				//var lerpsed = EaseFunctions.SmoothStart5(t) * 0.5f;
				var lerpsed = (1f - Mathf.Pow(1f - t, SlowExponent)) * SlowRatio;

				//Debug.Log("t :" + t);
				Time.timeScale = (1f - SlowRatio) + lerpsed;
			}
			else
			{
				Time.timeScale = 1;
			}

		}
	}


}
