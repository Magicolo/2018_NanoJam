using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateform : MonoBehaviour
{

	public MeshRenderer Mesh;
	public PolygonCollider2D[] Colliders { get { return GetComponentsInChildren<PolygonCollider2D>(); }}


	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
