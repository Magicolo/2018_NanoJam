using UnityEngine;

public class Player : MonoBehaviour
{
	public float Speed = 100;
	public Rigidbody2D Body;
	public string Horizontal = "Horizontal";
	public string Vertical = "Vertical";

	void FixedUpdate()
	{
		var input = new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
		Body.AddForce(input.normalized * Speed);

		var velocity = Body.velocity;
		var magnitude = velocity.magnitude;
		if (magnitude > Speed) Body.velocity = velocity / magnitude * Speed;
	}
}
