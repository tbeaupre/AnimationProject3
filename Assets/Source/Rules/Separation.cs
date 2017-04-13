using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation : FlockingRule {
	
	public Separation(Boid target) : base(target)
	{
	}

	public override Vector2 GetForce()
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
