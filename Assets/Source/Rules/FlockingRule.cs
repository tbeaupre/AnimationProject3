using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockingRule : Rule
{
	public virtual Vector2 GetForce(Boid target, List<Boid> neighbors)
	{
		return new Vector2(0, 0);
	}
}


