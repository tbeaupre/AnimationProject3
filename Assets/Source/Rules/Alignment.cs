using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alignment : FlockingRule {

	public override Vector2 GetForce(Boid target, List<Boid> neighbors)
	{
		if (neighbors.Count > 0)
		{
			Vector2 averageHeading = new Vector2(0, 0);
			foreach (Boid neighbor in neighbors)
			{
				averageHeading += neighbor.heading;
			}
			averageHeading /= neighbors.Count;

			return averageHeading - target.heading;
		} else
		{
			return new Vector2(0, 0);
		}
	}
}
