using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation : FlockingRule {

	public override Vector2 GetForce(Boid target, List<Boid> neighbors)
	{
		Vector2 force = new Vector2(0, 0);
		foreach (Boid neighbor in neighbors)
		{
			Vector2 direction = target.position - neighbor.position;
			force += direction.normalized / direction.magnitude;
		}
		return force;
	}
}
