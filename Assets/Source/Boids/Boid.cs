using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour {
	public float maxSpeed { get; set; }
	public float maxForce { get; set; }

	public BoidType type { get; set; }
	public BoidType preyType { get; set; }
	public BoidType predatorType { get; set; }

	public Vector2 position { get; set; }
	public Vector2 heading { get; set; }
	public Vector2 force { get; set; }

	// Initializes the boid from a set of values. Used at start.
	public void Init (BoidType type, float maxSpeed, float maxForce, Vector2 heading) {
		this.type = type;
		this.preyType = GetPreyType();
		this.predatorType = GetPredatorType();
		this.position = this.transform.position;
		this.maxSpeed = maxSpeed;
		this.maxForce = maxForce;
		this.heading = heading;
	}

	// Initializes the boid from another boid. Used in converting boids.
	public void Init(Boid boid, BoidType type)
	{
		this.maxSpeed = boid.maxSpeed;
		this.maxForce = boid.maxForce;

		this.type = type;
		this.preyType = GetPreyType();
		this.predatorType = GetPredatorType();

		this.position = boid.position;
		this.transform.position = position;
		this.heading = boid.heading;
		this.force = boid.force;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	public BoidType GetPreyType()
	{
		switch (type)
		{
		case(BoidType.BLUE):
			return BoidType.GREEN;
		case(BoidType.GREEN):
			return BoidType.RED;
		case(BoidType.RED):
			return BoidType.BLUE;
		default:
			return BoidType.GREEN;
		}
	}

	public BoidType GetPredatorType()
	{
		switch (type)
		{
		case(BoidType.BLUE):
			return BoidType.RED;
		case(BoidType.GREEN):
			return BoidType.BLUE;
		case(BoidType.RED):
			return BoidType.GREEN;
		default:
			return BoidType.RED;
		}
	}

	// The updates which come from the boidManager
	public void MgrUpdate() {
		// Respect max force
		force = Vector2.ClampMagnitude(force, maxForce);

		// Apply force and respect max speed
		heading += force;
		heading = Vector2.ClampMagnitude(heading, maxSpeed);

		// Reset 2D and 3D positions
		position += heading;
		this.transform.position = position;

		Vector3 eulerAngles = this.transform.eulerAngles;
		eulerAngles.z = Mathf.Tan(heading.y / heading.x);
		this.transform.eulerAngles = eulerAngles;
	}
}
