using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plateform : MonoBehaviour
{

	private MeshRenderer MeshR;
	public PolygonCollider2D[] Colliders { get { return GetComponentsInChildren<PolygonCollider2D>(); } }

	void Awake()
	{
		MeshR = GetComponentInChildren<MeshRenderer>();
		MeshR.material = new Material(MeshR.material);
	}


	void Start()
	{
		StartCoroutine(Logic());
	}

	private IEnumerator Logic()
	{
		yield return MoveLaPlatform();
		yield return Effects.LerpColor((c) => MeshR.material.color = c, Color.white, new Color(1, 1, 1, 0), 1);
		Destroy(gameObject);
	}

	private IEnumerator MoveLaPlatform()
	{

		while (transform.position.z > 0)
		{
			transform.position += Vector3.forward * Time.deltaTime * PlateformManager.Instance.PlateformMoveSpeed;
			yield return null;
		}

		var p = transform.position;
		transform.position = new Vector3(p.x, p.y, 0);

		foreach (var player in PlateformManager.Instance.AlivePlayer)
		{
			//Debug.Log("TOUCHE : " + player.Collider.IsTouching(Colliders[0]));
			if (Colliders.Any((c) => Physics2D.IsTouching(c, player.Collider))){
				Debug.Log("ASDASDADS");
				player.Kill();
			}
		}

	}

	void Update()
	{

	}
}
