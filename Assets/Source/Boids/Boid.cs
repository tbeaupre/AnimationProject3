using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boid : MonoBehaviour {
	[SerializeField] BoidMgr mgr;

	FlockingRuleSet ruleSet;

	public float senseRadius { get; set; }
	public BoidType type { get; set; }

	[SerializeField] public Vector2 position { get; set; }
	public Vector2 heading { get; set; }
	public float maxSpeed { get; set; }

	public void Init (BoidMgr mgr, BoidType type, float senseRadius, float maxSpeed, Vector2 heading) {
		Debug.Log("Initializing");
		this.mgr = mgr;
		this.type = type;
		this.position = this.transform.position;
		this.ruleSet = new FlockingRuleSet(this, 0.5f, 0.3f, 0.2f);
		this.senseRadius = senseRadius;
		this.maxSpeed = maxSpeed;
		this.heading = heading;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void MgrUpdate() {
		Debug.Log("Updating");
		ruleSet.UpdateNeighbors(mgr.FindNeighbors(this));

		Vector2 force = GetForce();
		heading += force.normalized;
		Vector2.ClampMagnitude(heading, maxSpeed);

		position += force;

		this.transform.position = position;
	}

	Vector2 GetForce()
	{
		return ruleSet.GetForce();
	}
}
