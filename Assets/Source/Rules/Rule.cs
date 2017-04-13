using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Rule {
	public Boid target;
	public virtual Vector2 GetForce()
	{
		return new Vector2(0, 0);
	}

	public Vector2 seek(Vector2 location)
	{
		Vector2 desired = location - target.position;
		return Vector2.ClampMagnitude(desired, target.maxSpeed);
	}
}
