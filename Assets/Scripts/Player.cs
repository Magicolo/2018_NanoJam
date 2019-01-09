using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum States
    {
        Alive,
        Dead
    }

    public bool IsMoving = false;
    public float MoveDirectionHA;

    public float Speed = 100;
    public float Bump = 100f;
    public SpriteRenderer Sprite;
    public Collider2D Collider;
    public Rigidbody2D Body;
    public SpriteAnimator Animator;
    public string Horizontal = "Horizontal";
    public string Vertical = "Vertical";

    public States State = States.Alive;
    float _respawn;
    Transform _killer;

    public Color PlayerColor;
    public int Score;

    private IEnumeratorQueue Animations = new IEnumeratorQueue();

    public bool IsAlive => State == States.Alive;

    void Update()
    {
        switch (State)
        {
            case States.Alive:
                Body.simulated = true;
                Collider.enabled = true;
                break;
            case States.Dead:
                _respawn -= Time.deltaTime;
                Body.simulated = false;
                Collider.enabled = false;
                break;
        }

        Animations.Update();
    }

    void FixedUpdate()
    {
        var input = new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
        MoveDirectionHA = 0;
        MoveDirectionHA = input.x;
        switch (State)
        {
            case States.Alive:
                IsMoving = input.magnitude > 0;
                Body.AddForce(input.normalized * Speed);
                break;
            case States.Dead:
                IsMoving = false;
                if (_killer)
                {
                    var position = transform.position;
                    position.z = _killer.position.z - 2.5f;
                    transform.position = position;
                }
                if (_respawn <= 0f && input.magnitude > 0.1f) Revive();
                break;
        }

        // var velocity = Body.velocity;
        // var magnitude = velocity.magnitude;
        // if (magnitude > Speed) Body.velocity = velocity.normalized * Speed;
    }

    public void Revive()
    {
        State = States.Alive;
        Animator.enabled = true;

        var position = transform.position;
        position.z = 0f;
        transform.position = position;

        Animations.Enqueue(ShowAnimation());
    }

    public void Kill(Transform killer)
    {
        State = States.Dead;
        _respawn = 1f;
        _killer = killer;
        Score = 0;
        Animations.Enqueue(KillAnimation());
    }

    IEnumerator KillAnimation()
    {
        Animator.enabled = false;
        Animator.Sprite.sprite = Animator.DeathSprite;
        foreach (var _ in Effects.LerpColor(ChangeColor, Color.white, Color.black, 1))
            yield return null;
        foreach (var _ in (Effects.LerpColor(ChangeColor, Color.black, new Color(0, 0, 0, 0), 0.5f)))
            yield return null;
    }


    IEnumerator ShowAnimation()
    {
        Animator.enabled = true;
        Animator.Sprite.sprite = Animator.Sprites[0];
        foreach (var _ in Effects.LerpColor(ChangeColor, new Color(0, 0, 0, 0), Color.white, 1))
        {
            yield return null;
        }
    }

    IEnumerator BumpRoutine(Player player, ContactPoint2D[] contacts)
    {
        var modifier = player.Body.velocity.magnitude * Bump;
        yield return null;

        foreach (var contact in contacts)
        {
            var force = -contact.normal * modifier;
            player.Body.AddForce(force, ForceMode2D.Impulse);
        }
    }

    void ChangeColor(Color color) => Animator.Sprite.color = color;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.rigidbody == null) return;
        var player = collision.rigidbody.GetComponentInChildren<Player>();
        if (player == null) return;
        StartCoroutine(BumpRoutine(player, collision.contacts));
    }
}
