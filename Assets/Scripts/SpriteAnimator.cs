using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
	public SpriteRenderer Sprite;
	public float Speed;
	public Sprite[] Sprites;

	void Update()
	{
		var index = (int)(Time.time * Speed) % Sprites.Length;
		Sprite.sprite = Sprites[index];
	}
}
