using System.Collections;
using System.Linq;
using UnityEngine;

public class Plateform : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public SpriteMask Mask;
    public PolygonCollider2D[] Colliders => GetComponentsInChildren<PolygonCollider2D>();

    bool _hasKilled;

    //void Start() => StartCoroutine(Logic());

    private IEnumerator Logic()
    {
        yield return MoveLaPlatform();
        yield return Effects.LerpColor((c) => Sprite.color = c, Color.white, new Color(1, 1, 1, 0), 1);
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
        KillPlayers();
    }

    void KillPlayers()
    {
        foreach (var c in Colliders)
        {
            var contacts = new Collider2D[16];

            if (c.OverlapCollider(new ContactFilter2D { }, contacts) > 0)
            {
                var tokill = contacts.Select(contact => contact?.GetComponentInParent<Player>()).Where(player => player != null && player.State.Equals(Player.States.Alive));
                foreach (var tk in tokill) tk.Kill(transform);
            }
        }

        foreach (var player in FindObjectsOfType<Player>())
            if (player.IsAlive) player.Score++;
    }

    void Update()
    {
        if (transform.position.z < 0f && !_hasKilled)
        {
            _hasKilled = true;
            KillPlayers();
        }
    }
}
