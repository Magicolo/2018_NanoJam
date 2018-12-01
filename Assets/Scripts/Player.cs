using UnityEngine;

public class Player : MonoBehaviour
{
	public enum States
	{
		Alive,
		Dead
	}

	public float Speed = 100;
	public SpriteRenderer Sprite;
	public Collider2D Collider;
	public Rigidbody2D Body;
	public string Horizontal = "Horizontal";
	public string Vertical = "Vertical";

	public States State = States.Alive;
	float _respawn;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.K)) Kill();

		switch (State)
		{
			case States.Alive:
				Body.simulated = true;
				Collider.enabled = true;
				Sprite.color = Color.Lerp(Sprite.color, Color.white, 3f * Time.deltaTime);
				break;
			case States.Dead:
				_respawn -= Time.deltaTime;
				Body.simulated = false;
				Collider.enabled = false;
				Sprite.color = Color.Lerp(Sprite.color, Color.black, 3f * Time.deltaTime);
				break;
		}
	}

	void FixedUpdate()
	{
		var input = new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));

		switch (State)
		{
			case States.Alive:
				Body.AddForce(input.normalized * Speed);
				break;
			case States.Dead:
				if (_respawn <= 0f && input.magnitude > 0.1f) Revive();
				break;
		}

		var velocity = Body.velocity;
		var magnitude = velocity.magnitude;
		if (magnitude > Speed) Body.velocity = velocity.normalized * Speed;
	}

	public void Revive() => State = States.Alive;

	public void Kill()
	{
		State = States.Dead;
		_respawn = 1f;
	}
}
