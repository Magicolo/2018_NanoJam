using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plateform : MonoBehaviour
{

	public MeshRenderer Mesh;
	public PolygonCollider2D[] Colliders { get { return GetComponentsInChildren<PolygonCollider2D>(); } }


	void Start()
	{

	}


	void Update()
	{
		transform.position += Vector3.forward * Time.deltaTime * PlateformManager.Instance.PlateformMoveSpeed;

		if (transform.position.z <= 0)
		{
			var p = transform.position;
			transform.position = new Vector3(p.x, p.y, 0);

			foreach (var player in PlateformManager.Instance.Players)
			{
				/* if (Colliders.Any((c) => c.IsTouching(player.)){
					player
				}*/
			}


		}
	}
}
