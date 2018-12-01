using UnityEngine;

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

	void Update()
	{
	}
}
