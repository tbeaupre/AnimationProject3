using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary
{
	float width;
	float threshold;
	float height;

	public Boundary (int width, int height, float threshold)
	{
		this.width = width;
		this.height = height;
		this.threshold = threshold;
	}

	public Vector2 GetForce(Boid target)
	{
		Vector2 force = new Vector2(0, 0);
		if (Mathf.Abs(target.position.x) > (width - threshold))
		{
			if (target.position.x > 1)
			{
				force.x = -Mathf.Abs(target.heading.x);
			} else
			{
				force.x = Mathf.Abs(target.heading.x);
			}
		}
		if (Mathf.Abs(target.position.y) > (height - threshold))
		{
			if (target.position.y > 1)
			{
				force.y = -Mathf.Abs(target.heading.y);
			} else
			{
				force.y = Mathf.Abs(target.heading.y);
			}
		}
		return force;
	}
}

