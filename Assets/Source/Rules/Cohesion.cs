using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cohesion : FlockingRule {

	// Finds the average position of all neighbors and finds the force necessary to get there
	public override Vector2 GetForce(Boid target, List<Boid> neighbors)
	{
		Vector2 center = new Vector2(0, 0);
		if (neighbors.Count > 0)
		{
			foreach (Boid neighbor in neighbors)
			{
				center += neighbor.position;
			}
			center /= neighbors.Count;
			return Seek(target, center);
		} else
		{
			return center;
		}
	}
}
