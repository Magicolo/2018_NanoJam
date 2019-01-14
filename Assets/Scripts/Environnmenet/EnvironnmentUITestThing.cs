using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironnmentUITestThing : MonoBehaviour
{

	public Text Text;


	void Start()
	{

	}


	void Update()
	{
		var env = EnvironnementManager.Instance.Environnements;
        if(env.Count == 0)
            Text.text = "Missing Environnement or Loading";
            else
            Text.text = $"{env.Count} environnement loaded";
	}
}
