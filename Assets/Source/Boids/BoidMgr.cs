using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMgr : MonoBehaviour {
	List<Boid> boids = new List<Boid>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Boid boid in boids)
		{
			//boid.Update();
		}
	}

	public List<Boid> FindNeighbors(Boid target)
	{
		List<Boid> neighbors = new List<Boid>();

		foreach (Boid boid in boids)
		{
			if ((boid.type == target.type) && (Distance(target, boid) <= target.senseRadius))
			{
				neighbors.Add(boid);
			}
		}

		return neighbors;
	}

	float Distance(Boid b1, Boid b2)
	{
		return Vector2.Distance(b1.position, b2.position);
	}
}
