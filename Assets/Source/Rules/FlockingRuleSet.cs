using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingRuleSet : FlockingRule
{
	Separation sRule;
	Alignment aRule;
	Cohesion cRule;

	float sWeight;
	float aWeight;
	float cWeight;

	public FlockingRuleSet (Boid target, float separationWeight, float alignmentWeight, float cohesionWeight) : base(target)
	{
		this.sWeight = separationWeight;
		this.aWeight = alignmentWeight;
		this.cWeight = cohesionWeight;

		this.sRule = new Separation(target);
		this.aRule = new Alignment(target);
		this.cRule = new Cohesion(target);
	}

	public override Vector2 GetForce ()
	{
		Vector2 separationForce = sRule.GetForce() * sWeight;
		Vector2 alignmentForce = aRule.GetForce() * aWeight;
		Vector2 cohesionForce = cRule.GetForce() * cWeight;
		Vector2 sumForce = separationForce + alignmentForce + cohesionForce;
		return Vector2.ClampMagnitude(sumForce, target.maxSpeed);
	}

	public override void UpdateNeighbors (List<Boid> neighbors)
	{
		base.UpdateNeighbors(neighbors);
		sRule.UpdateNeighbors(neighbors);
		aRule.UpdateNeighbors(neighbors);
		cRule.UpdateNeighbors(neighbors);
	}
}


