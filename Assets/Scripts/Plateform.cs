using System.Collections;
using System.Linq;
using UnityEngine;

public class Plateform : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public SpriteMask Mask;

    bool _hasKilled;
	public int NbPixelForContact = 1;



    void TryKillPlayers()
    {
        foreach (var player in PlayerManager.Instance.AlivePlayers)
        {
            if(PixelPerfectCheck.Collides(this,player,NbPixelForContact))
                player.Kill(transform);

        }

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
