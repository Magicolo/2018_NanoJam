using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Plateform : MonoBehaviour
{

	private SpriteRenderer SpriteR;
	public PolygonCollider2D[] Colliders { get { return GetComponentsInChildren<PolygonCollider2D>(); } }

	void Awake()
	{
		SpriteR = GetComponentInChildren<SpriteRenderer>();
	}


	void Start()
	{
		StartCoroutine(Logic());
	}

	private IEnumerator Logic()
	{
		yield return MoveLaPlatform();
		yield return Effects.LerpColor((c) => SpriteR.color = c, Color.white, new Color(1, 1, 1, 0), 1);
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


		foreach (var c in Colliders)
		{
			var contacts = new Collider2D[16];

			if (c.OverlapCollider(new ContactFilter2D { }, contacts) > 0)
			{
				var tokill = contacts.Select(contact => contact?.GetComponentInParent<Player>()).Where(player => player != null && player.State.Equals(Player.States.Alive));
				foreach (var tk in tokill)
					tk.Kill();
			}
		}

		foreach (var player in PlateformManager.Instance.AlivePlayer)
			player.Score++;
	}

	void Update()
	{

	}
}
