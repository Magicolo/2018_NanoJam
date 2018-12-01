using UnityEngine;

public class Singleton<TSelf> : MonoBehaviour where TSelf : Singleton<TSelf>
{
	static TSelf instance;

	public static TSelf Instance
	{
		get
		{
			if (instance == null) instance = FindObjectOfType<TSelf>();
			return instance;
		}
	}

	protected virtual void Awake() { instance = this as TSelf; }
}