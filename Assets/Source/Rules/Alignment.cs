using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alignment : FlockingRule {

	public override Vector2 GetForce(Boid target, List<Boid> neighbors)
	{
		if (neighbors.Count > 0)
		{
			Vector2 averageHeading;
			Vector2 sum = new Vector2(0, 0);
			foreach (Boid neighbor in neighbors)
			{
				sum += neighbor.heading;
			}
			averageHeading = sum / neighbors.Count;

			return averageHeading - target.heading;
		} else
		{
			return new Vector2(0, 0);
		}
	}
}
