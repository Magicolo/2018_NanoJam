using System.Collections;
using System.Linq;
using UnityEngine;

public class Plateform : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public SpriteMask Mask;

    bool _hasKilled;



    void TryKillPlayers()
    {
        foreach (var player in PlayerManager.Instance.AlivePlayers)
        {
            if(PixelPerfectCheck.Collides(this,player,1))
                player.Kill(transform);

        }
       /*  foreach (var c in Colliders)
        {
            var contacts = new Collider2D[16];

            if (c.OverlapCollider(new ContactFilter2D { }, contacts) > 0)
            {
                var tokill = contacts.Select(contact => contact?.GetComponentInParent<Player>()).Where(player => player != null && player.State.Equals(Player.States.Alive));
                foreach (var tk in tokill) tk.Kill(transform);
            }
        } */

        foreach (var player in FindObjectsOfType<Player>())
            if (player.IsAlive) player.Score++;
    }

    void Update()
    {
        if (transform.position.z < 0f && !_hasKilled)
        {
            _hasKilled = true;
            TryKillPlayers();
        }
    }
}
