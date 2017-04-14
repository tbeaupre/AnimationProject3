using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary
{
	List<ForceField> walls = new List<ForceField>(4);
	public Boundary (int width, int height, float threshold)
	{
		walls.Add(new ForceField(true, -height, threshold)); // Bottom wall
		walls.Add(new ForceField(true, height, threshold)); // Top Wall
		walls.Add(new ForceField(false, -width, threshold)); // Left Wall
		walls.Add(new ForceField(false, width, threshold)); // Right Wall
	}

	public Vector2 GetForce(Boid target)
	{
		Vector2 result = new Vector2(0, 0);
		Vector2 force;

		// Handle Top and Bottom walls
		result = walls[0].GetForce(target);
		if (result == new Vector2(0, 0))
		{
			result = walls[1].GetForce(target);
		}

		// Handle Left and Right walls
		force = walls[2].GetForce(target);
		if (force != new Vector2(0, 0))
		{
			result += force;
		} else
		{
			result += walls[3].GetForce(target);
		}

		return result;
	}
}

