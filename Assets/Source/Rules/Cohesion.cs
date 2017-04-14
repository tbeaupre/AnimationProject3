using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cohesion : FlockingRule {

	public override Vector2 GetForce(Boid target, List<Boid> neighbors)
	{
		Vector2 center = new Vector2(0, 0);
		foreach (Boid neighbor in neighbors)
		{
			center += neighbor.position;
		}
		center /= neighbors.Count;
		return seek(target, center);
	}
}
