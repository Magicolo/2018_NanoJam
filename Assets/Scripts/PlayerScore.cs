using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{

	public Text ScoreText;

	public Player MyPlayer;
	public int PlayerIndex;

	private Color BaseColor;

	private Queue<IEnumerator> ToAnimate = new Queue<IEnumerator>();
	private IEnumerator CurrentAnimation;

	private Player.States LastKnownState = Player.States.Dead;
	private int LastKnownScore = 0;

	void Awake()
	{
		BaseColor = ScoreText.color;
	}


	void Start()
	{
		MyPlayer = PlayerManager.Instance.GetPlayer(PlayerIndex);
		ScoreText.text = "0";
		ScoreText.enabled = false;
	}

	IEnumerator AnimateScore(int newScore)
	{
		ScoreText.text = newScore + "";
		yield return null;
	}

	void SetTextColor(Color c) => ScoreText.color = c;


	void Update()
	{
		if (MyPlayer == null) return;

		if (LastKnownState != MyPlayer.State)
		{
			if (MyPlayer.State == Player.States.Alive)
			{
				ScoreText.enabled = true;
				ScoreText.text = "0";
				ToAnimate.Enqueue(Effects.LerpColor(SetTextColor, BaseColor, MyPlayer.PlayerColor, 1));
			}
			else
			{
				LastKnownScore = 0;
				ToAnimate.Enqueue(Effects.LerpColor(SetTextColor, MyPlayer.PlayerColor, BaseColor, 1));
			}
			LastKnownState = MyPlayer.State;
		}

		if (LastKnownScore != MyPlayer.Score)
		{
			LastKnownScore = MyPlayer.Score;
			ToAnimate.Enqueue(Effects.LerpColor(SetTextColor, MyPlayer.PlayerColor, Color.white, 0.3f));
			ToAnimate.Enqueue(AnimateScore(MyPlayer.Score));
			ToAnimate.Enqueue(Effects.LerpColor(SetTextColor, Color.white, MyPlayer.PlayerColor, 0.7f));
		}

		if (ToAnimate.Count != 0 && CurrentAnimation == null)
			CurrentAnimation = ToAnimate.Dequeue();

		if (CurrentAnimation != null && !CurrentAnimation.MoveNext())
			CurrentAnimation = null;

	}
}
