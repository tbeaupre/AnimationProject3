using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockingRule : Rule {
	public List<Boid> neighbors;

	public FlockingRule (Boid target)
	{
		this.target = target;
		this.neighbors = new List<Boid>();
	}

	public virtual void UpdateNeighbors(List<Boid> neighbors)
	{
		this.neighbors = neighbors;
	}
}
