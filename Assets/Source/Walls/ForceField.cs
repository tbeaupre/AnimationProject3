using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField
{
	bool horizontal;
	int value;
	float threshold;

	public ForceField (bool horizontal, int value, float threshold)
	{
		this.horizontal = horizontal;
		this.value = value;
		this.threshold = threshold;
	}

	public Vector2 GetForce(Boid target)
	{
		Vector2 force = new Vector2(0, 0);
		Vector2 position = GetPosition(target.position);


		if (Vector2.Distance(target.position, position) < threshold)
		{
			Vector2 direction = target.position - position;
			force = direction.normalized / direction.magnitude;
		}
		return force;
	}

	public Vector2 GetPosition(Vector2 targetPos)
	{
		if (horizontal)
		{
			return new Vector2(targetPos.x, value);
		} else
		{
			return new Vector2(value, targetPos.y);
		}
	}
}


