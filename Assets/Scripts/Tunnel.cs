﻿using System.Collections;
using UnityEngine;

public class Tunnel : MonoBehaviour
{
	public SpriteRenderer Particle;
	public Plateform[] Obstacles;
	public float Frequency = 10f;
	public float Duration = 1f;
	public float RandomColor = 0.1f;
	public float[] Angles = { 0f, 90f, 180f, 270f };
	public Gradient Gradient;
	public AnimationCurve Curve = AnimationCurve.Linear(0, 0, 1f, 1f);

	float _nextBackground;
	float _nextObstacle;

	void Start()
	{
		_nextBackground = Time.time;
		_nextObstacle = Time.time + 5f;
	}

	void Update()
	{
		while (_nextBackground < Time.time)
		{
			_nextBackground += 1f / Frequency;
			var target = new Vector3(0f, 0f, -25f);
			var angle = Angles[Random.Range(0, Angles.Length)];
			var sprite = Instantiate(Particle, transform);
			sprite.transform.eulerAngles = new Vector3(0f, 0f, angle);
			StartCoroutine(Spawn(sprite.transform, sprite, target));

			if (_nextObstacle < Time.time)
			{
				_nextObstacle += 5f;
				var index = Random.Range(0, Obstacles.Length);
				var obstacle = Instantiate(Obstacles[index], transform);
				obstacle.Mask.sprite = sprite.sprite;
				StartCoroutine(Spawn(obstacle.transform, obstacle.Sprite, target));
			}
		}
	}

	IEnumerator Spawn(Transform instance, SpriteRenderer sprite, Vector3 target)
	{
		var initialPosition = instance.position;
		var initialColor = sprite.color + Random.ColorHSV(-RandomColor, RandomColor, -RandomColor, RandomColor, -RandomColor, RandomColor);
		var difference = target - initialPosition;
		var total = difference.magnitude;

		while (true)
		{
			var source = instance.position;
			var distance = (target - source).magnitude;
			var ratio = 1f - Mathf.Clamp01(distance / total);
			if (ratio > 0.995f) break;

			var time = ratio + (1f / Duration) * Time.deltaTime;
			var position = difference * Curve.Evaluate(time) + initialPosition;
			var color = initialColor * Gradient.Evaluate(time);
			instance.position = position;
			sprite.color = color;
			yield return null;
		}

		instance.position = target;
		yield return null;
		Destroy(instance.gameObject);
	}
}
