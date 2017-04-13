using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cohesion : FlockingRule {

	public Cohesion(Boid target) : base(target)
	{
	}

	public override Vector2 GetForce()
	{
		Vector2 center = new Vector2(0, 0);
		foreach (Boid neighbor in neighbors)
		{
			center += neighbor.position;
		}
		center /= neighbors.Count;
		return seek(center);
	}
}
