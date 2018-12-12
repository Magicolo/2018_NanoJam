using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnumeratorQueue
{


	private Queue<IEnumerator> Queue = new Queue<IEnumerator>();
	private IEnumerator Current;


	public void Enqueue(IEnumerator enumator)
	{
		Queue.Enqueue(enumator);
	}

	public void Update()
	{
		if (Queue.Count != 0 && Current == null)
			Current = Queue.Dequeue();

		if (Current != null && !Current.MoveNext())
			Current = null;
	}

	public void Clear()
	{
		Queue.Clear();
		Current = null;
	}
}
