using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
	private Player MyPlayer;
	public SpriteRenderer Sprite;
	public float Speed;
	public Sprite[] Sprites;
	public Sprite[] SpritesTilt;

	public bool IsMoving { get { return GetComponentInParent<Player>().IsMoving; } }


	void Awake()
	{
		MyPlayer = GetComponentInParent<Player>();
	}


	void Update()
	{
		Sprite.flipX = false;
		if (IsMoving && MyPlayer.MoveDirectionHA != 0)
		{
			Sprite.flipX = MyPlayer.MoveDirectionHA < 0;
			var index = (int)(Time.time * Speed) % SpritesTilt.Length;
			Sprite.sprite = SpritesTilt[index];
		}
		else
		{
			var index = (int)(Time.time * Speed) % Sprites.Length;
			Sprite.sprite = Sprites[index];
		}

	}
}
