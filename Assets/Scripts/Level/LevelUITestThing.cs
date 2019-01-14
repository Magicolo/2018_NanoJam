using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUITestThing : MonoBehaviour
{

	public Text Text;


	void Start()
	{

	}


	void Update()
	{
		var env = LevelManager.Instance.Levels;
        if(env.Count == 0)
            Text.text = "Missing Environnement or Loading";
            else
            Text.text = $"{env.Count} environnement loaded";
	}
}
