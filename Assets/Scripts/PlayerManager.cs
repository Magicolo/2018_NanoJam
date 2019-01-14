using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class PlayerManager : Singleton<PlayerManager>
{
	public Player[] Players;

	public Player GetPlayer(int id)
	{
		if (id >= Players.Length)
			return null;
		else
			return Players[id];
	}

	public List<Player> AlivePlayers{
		get{
			return Players.Where(player=>player!= null && player.IsAlive).ToList();
		}
	}

	void Update()
	{
	}
}
