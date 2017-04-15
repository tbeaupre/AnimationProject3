using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {
	public float r;
	public Vector2 center;

	void Start()
	{
		this.r = this.transform.localScale.x / 2;
		this.center = this.transform.position;
	}

	// Finds the nearest point on the boundary circle, doubles its distance from the center, and seeks towards that point.
	public Vector2 GetForce(Boid target)
	{
		Vector2 result = new Vector2(0, 0);

		if (Vector2.Distance(target.position, center) < (r * 1.5f))
		{
			Vector3 C = center;
			Vector3 P = target.position;
			float distToCenter = (C - P).magnitude;
			float distToCenterSqr = (C - P).sqrMagnitude;

			float k = Mathf.Sqrt(distToCenterSqr - (r * r));
			float t = ((k * k) - (r * r) - distToCenterSqr) / (-2 * distToCenter);
			float s = Mathf.Sqrt((r * r) - (t * t));

			Vector3 U = (C - P).normalized;
			Vector3 V = target.heading;
			Vector3 W = Vector3.Cross(Vector3.Cross(U, V), U).normalized;

			Vector3 B = P + (distToCenter - t) * U + s * W;
			Vector2 seek = B + (B - C);

			result = Seek(target, seek);
		}
		return result;
	}

	public Vector2 Seek(Boid target, Vector2 location)
	{
		Vector2 desired = location - target.position;
		return Vector2.ClampMagnitude(desired, target.maxSpeed);
	}
}
