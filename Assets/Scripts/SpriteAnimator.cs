using System;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
	private Player MyPlayer;
	public SpriteRenderer Sprite;
	public float Speed;
	public float FramePerSecond;
	public Sprite[] Straight;
	public Sprite TransitionSprite;
	public Sprite[] SpritesTilt;
	public Sprite DeathSprite;

	public AnimationState AnimState = AnimationState.Center;
	public enum AnimationState { Left, TransitionLeft, Center, TransitionRight, Right }

	public bool IsMoving { get { return GetComponentInParent<Player>().IsMoving; } }
	private bool IsInTransition { get { return AnimState == AnimationState.TransitionLeft || AnimState == AnimationState.TransitionRight; } }

	private float LastDirection = 0;

	public float SpriteCenteringThreshold;
	public float NextAnimationUpdate;
	void Awake()
	{
		MyPlayer = GetComponentInParent<Player>();
		NextAnimationUpdate = Time.time;
	}


	void Update()
	{
		Sprite.flipX = false;
		if (!MyPlayer.IsActive)
		{
			Sprite.sprite = null;
		}
		else if (!MyPlayer.IsAlive)
		{
			Sprite.sprite = DeathSprite;
		}
		else
		{
			if (LastDirection != SignDirect(MyPlayer.MoveDirectionInputRaw))
			{
				NextAnimationUpdate += 1 / FramePerSecond;
			}

			while (NextAnimationUpdate < Time.time)
			{
				NextAnimationUpdate += 1 / FramePerSecond;
				//print(MyPlayer.Body.velocity.x);
				if (MyPlayer.MoveDirectionInputRaw == 0)
				{
					if (Mathf.Abs(MyPlayer.Body.velocity.x) < SpriteCenteringThreshold || IsInTransition)
						MoveAnimCenter();
				}
				else if (MyPlayer.Body.velocity.x > 0)
					MoveAnimRight();
				else if (MyPlayer.Body.velocity.x < 0)
					MoveAnimLeft();
			}

			ShowSprite();
		}
		if (MyPlayer.MoveDirectionInputRaw == 0)
			LastDirection = 0;
		else
			LastDirection = SignDirect(MyPlayer.MoveDirectionInputRaw);

	}

	private float SignDirect(float value)
	{
		if (value == 0)
			return 0;
		else if (value < 0)
			return -1;
		else
			return 1;
	}

	private void ShowSprite()
	{
		if (AnimState == AnimationState.Right)
			ShowSprite(SpritesTilt, false);
		else if (AnimState == AnimationState.TransitionRight)
			ShowSprite(TransitionSprite, false);
		else if (AnimState == AnimationState.Center)
			ShowSprite(Straight, false);
		else if (AnimState == AnimationState.TransitionLeft)
			ShowSprite(TransitionSprite, true);
		else if (AnimState == AnimationState.Left)
			ShowSprite(SpritesTilt, true);
	}

	private void ShowSprite(Sprite sprite, bool flip)
	{
		Sprite.flipX = flip;
		Sprite.sprite = sprite;
	}

	private void ShowSprite(Sprite[] sprites, bool flip)
	{
		Sprite.flipX = flip;
		var index = (int)(Time.time * Speed) % sprites.Length;
		Sprite.sprite = sprites[index];
	}

	private void MoveAnimLeft()
	{
		if (AnimState == AnimationState.Right)
			AnimState = AnimationState.TransitionRight;
		else if (AnimState == AnimationState.TransitionRight)
			AnimState = AnimationState.Center;
		else if (AnimState == AnimationState.Center)
			AnimState = AnimationState.TransitionLeft;
		else if (AnimState == AnimationState.TransitionLeft)
			AnimState = AnimationState.Left;
	}

	private void MoveAnimCenter()
	{
		if (AnimState == AnimationState.Right)
			AnimState = AnimationState.TransitionRight;
		else if (AnimState == AnimationState.TransitionRight)
			AnimState = AnimationState.Center;
		else if (AnimState == AnimationState.Left)
			AnimState = AnimationState.TransitionLeft;
		else if (AnimState == AnimationState.TransitionLeft)
			AnimState = AnimationState.Center;
	}

	private void MoveAnimRight()
	{
		if (AnimState == AnimationState.Left)
			AnimState = AnimationState.TransitionLeft;
		else if (AnimState == AnimationState.TransitionLeft)
			AnimState = AnimationState.Center;
		else if (AnimState == AnimationState.Center)
			AnimState = AnimationState.TransitionRight;
		else if (AnimState == AnimationState.TransitionRight)
			AnimState = AnimationState.Right;
	}
}
