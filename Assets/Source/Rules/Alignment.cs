using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alignment : FlockingRule {

	public Alignment(Boid target) : base(target)
	{
	}

	public override Vector2 GetForce()
	{
		Vector2 averageHeading = new Vector2(0, 0);
		foreach (Boid neighbor in neighbors)
		{
			averageHeading += neighbor.heading;
		}
		averageHeading /= neighbors.Count;

		return averageHeading - target.heading;
	}
}
