using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boid : MonoBehaviour {
	public float maxSpeed { get; set; }
	public float maxForce { get; set; }

	public BoidType type { get; set; }

	public Vector2 position { get; set; }
	public Vector2 heading { get; set; }
	public Vector2 force { get; set; }

	public void Init (BoidType type, float maxSpeed, float maxForce, Vector2 heading) {
		this.type = type;
		this.position = this.transform.position;
		this.maxSpeed = maxSpeed;
		this.maxForce = maxForce;
		this.heading = heading;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void MgrUpdate() {
		// Respect max force
		force = Vector2.ClampMagnitude(force, maxForce);

		// Apply force and respect max speed
		heading += force;
		Vector2.ClampMagnitude(heading, maxSpeed);

		// Reset 2D and 3D positions
		position += heading;
		this.transform.position = position;

		Vector3 eulerAngles = this.transform.eulerAngles;
		eulerAngles.z = Mathf.Tan(heading.y / heading.x);
		this.transform.eulerAngles = eulerAngles;
	}
}
