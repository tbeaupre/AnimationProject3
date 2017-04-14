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

	public FlockingRuleSet (float separationWeight, float alignmentWeight, float cohesionWeight)
	{
		this.sWeight = separationWeight;
		this.aWeight = alignmentWeight;
		this.cWeight = cohesionWeight;

		this.sRule = new Separation();
		this.aRule = new Alignment();
		this.cRule = new Cohesion();
	}

	public override Vector2 GetForce (Boid target, List<Boid> neighbors)
	{
		if (neighbors.Count > 0)
		{
			Vector2 separationForce = sRule.GetForce(target, neighbors) * sWeight;
			Vector2 alignmentForce = aRule.GetForce(target, neighbors) * aWeight;
			Vector2 cohesionForce = cRule.GetForce(target, neighbors) * cWeight;
			Vector2 sumForce = separationForce + alignmentForce + cohesionForce;
			return Vector2.ClampMagnitude(sumForce, target.maxForce);
		} else
		{
			return new Vector2(0, 0);
		}
	}
}


