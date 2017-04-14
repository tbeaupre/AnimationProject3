using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rule {
	const float DT = 0.1f;

	public virtual Vector2 GetForce(Boid target)
	{
		return new Vector2(0, 0);
	}

	public Vector2 Seek(Boid target, Vector2 location)
	{
		Vector2 desired = location - target.position;
		return Vector2.ClampMagnitude(desired, target.maxSpeed);
	}

	public Vector2 Flee(Boid target, Vector2 location)
	{
		Vector2 desired = Vector2.ClampMagnitude(target.position - location, target.maxSpeed);
		return desired - target.heading;
	}

	public Vector2 Pursue(Boid pursuer, Boid evader)
	{
		if (evader != null)
		{
			return Seek(pursuer, evader.position + (evader.heading * DT));
		} else
		{
			return new Vector2(0, 0);
		}
	}

	public Vector2 Evade(Boid evader, Boid pursuer)
	{
		if (pursuer != null)
		{
			return Flee(evader, pursuer.position + (pursuer.heading * DT));
		} else
		{
			return new Vector2(0, 0);
		}
	}
}
