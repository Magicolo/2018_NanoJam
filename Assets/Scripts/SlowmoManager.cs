using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SlowmoManager : Singleton<SlowmoManager>
{

	public float SlowmoStartDistance;
	public float MaxDistanceDistance;

	void Update()
	{
		var plateforms = (Plateform[])GameObject.FindObjectsOfType(typeof(Plateform));
		if (plateforms.Count() != 0)
		{
			var first = plateforms.First();
			float z = first.transform.position.z;
			if (z < SlowmoStartDistance && z > MaxDistanceDistance)
			{
				float t = 0;
				if (z > 0)
					t = z / SlowmoStartDistance;
				else
					t = z / MaxDistanceDistance;

				float lerpsed = EaseFunctions.SmoothStart5(t) * 0.5f;
				//Debug.Log("t :" + t);
				Time.timeScale = 0.5f + lerpsed;
			}
			else
			{
				Time.timeScale = 1;
			}

		}
	}


}
