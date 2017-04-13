using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boid : MonoBehaviour {
	[SerializeField] BoidMgr mgr;

	FlockingRuleSet ruleSet;

	public float senseRadius { get; set; }
	public BoidType type { get; set; }

	public Vector2 position { get; set; }
	public Vector2 heading { get; set; }
	public float maxSpeed { get; set; }

	public void Init (BoidMgr mgr, BoidType type) {
		this.mgr = mgr;
		this.type = type;
	}

	// Use this for initialization
	void Start () {
		this.ruleSet = new FlockingRuleSet(this, 1, 1, 1);
		this.senseRadius = 100;
	}
	
	// Update is called once per frame
	void Update () {
		ruleSet.UpdateNeighbors(mgr.FindNeighbors(this));

		Vector2 force = GetForce();
		heading = force.normalized;

		position += force;

		this.transform.position = position;
	}

	Vector2 GetForce()
	{
		return ruleSet.GetForce();
	}
}
