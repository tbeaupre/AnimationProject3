using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMgr : MonoBehaviour {
	// Prefabs for instantiating new boids
	public Boid redBoidPrefab;
	public Boid blueBoidPrefab;
	public Boid greenBoidPrefab;

	// Screen information
	const int WIDTH = 134;
	const int HEIGHT = 100;

	// Boid properties
	const int QUANTITY = 10;
	const float SENSE_RADIUS = 50;
	const float COLLISION_RADIUS = 1;
	const float MAX_SPEED = 1.5f;
	const float MAX_FORCE = 2.5f;

	// Boundary Rule
	const float BOUNDARY_WEIGHT = 2f;
	Boundary boundary = new Boundary(WIDTH, HEIGHT, MAX_SPEED * 2);

	// Pursuit Rule
	const float EVADE_WEIGHT = 0.7f;
	const float PURSUE_WEIGHT = 0.5f;
	Rule pursueEvadeRule = new Rule();

	// Obstacle
	public Obstacle obstacle;
	const float OBSTACLE_WEIGHT = 1f;

	// Flocking Rule Set
	const float SEPARATION_WEIGHT = 1f;
	const float ALIGNMENT_WEIGHT = 0.3f;
	const float COHESION_WEIGHT = 0.3f;
	Separation sepRule = new Separation();
	Alignment aliRule = new Alignment();
	Cohesion cohRule = new Cohesion();

	List<Boid> boids = new List<Boid>(); // updated list of boids
	List<Boid> convertedBoids = new List<Boid>();

	// Use this for initialization
	void Start () {
		Random.InitState(1);
		CreateBoids(BoidType.BLUE, QUANTITY);
		CreateBoids(BoidType.RED, QUANTITY);
		CreateBoids(BoidType.GREEN, QUANTITY);
	}

	void CreateBoids (BoidType type, int quantity)
	{
		Boid boidPrefab = GetBoidPrefab(type);

		Quaternion rot = new Quaternion();
		for (int i = 0; i < quantity; i++)
		{
			Boid boidClone = Object.Instantiate<Boid>(boidPrefab, GetRandomPos(), rot);
			Debug.Log(string.Format("Drawing boid {0}", i));
			boidClone.Init(type, MAX_SPEED, MAX_FORCE, GetRandomHeading()); 
			boids.Add(boidClone);
		}
	}

	Boid GetBoidPrefab(BoidType type)
	{
		switch(type)
		{
		case(BoidType.BLUE):
			return blueBoidPrefab;
		case(BoidType.GREEN):
			return greenBoidPrefab;
		case(BoidType.RED):
			return redBoidPrefab;
		default:
			return blueBoidPrefab;
		}
	}

	Vector2 GetRandomPos()
	{
		int x = Random.Range(-WIDTH, WIDTH);
		int y = Random.Range(-HEIGHT, HEIGHT);
		Vector2 result = new Vector2(x, y);
		while (Vector2.Distance(result, new Vector2(0, 0)) < 30)
		{
			result *= 1.5f;
		}
		return result;
	}

	Vector2 GetRandomHeading()
	{
		float x = Random.Range(-MAX_SPEED, MAX_SPEED);
		float y = Random.Range(-MAX_SPEED, MAX_SPEED);
		Vector2 heading = new Vector2(x, y);
		return Vector2.ClampMagnitude(heading, MAX_SPEED);
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Boid boid in boids)
		{
			boid.force = GetBoidForce(boid);
			boid.MgrUpdate();
			CollisionDetect(boid);
		}
		foreach (Boid boid in convertedBoids)
		{
			if (boids.Contains(boid))
			{
				boids.Remove(boid);

				// Create a new boid, initialize it based on the old one and add it to the list.
				Boid targetClone = Object.Instantiate<Boid>(GetBoidPrefab(boid.predatorType), boid.position, new Quaternion());
				Debug.Log("Converting Boid!");
				targetClone.Init(boid, boid.predatorType);
				boids.Add(targetClone);

				Destroy(boid.gameObject);
			}
		}
	}

	Vector2 GetBoidForce(Boid boid)
	{
		// Boundaries
		Vector2 force = boundary.GetForce(boid) * BOUNDARY_WEIGHT;
		if (force.magnitude >= MAX_FORCE)
		{
			return Vector2.ClampMagnitude(force, MAX_FORCE);
		}

		// Center Obstacle
		force += obstacle.GetForce(boid) * OBSTACLE_WEIGHT;
		if (force.magnitude >= MAX_FORCE)
		{
			return Vector2.ClampMagnitude(force, MAX_FORCE);
		}

		// Evasion and Pursuit
		force += pursueEvadeRule.Evade(boid, FindClosestOfType(boid, boid.predatorType)) * EVADE_WEIGHT;
		if (force.magnitude >= MAX_FORCE)
		{
			return Vector2.ClampMagnitude(force, MAX_FORCE);
		}
		force += pursueEvadeRule.Pursue(boid, FindClosestOfType(boid, boid.preyType)) * PURSUE_WEIGHT;
		if (force.magnitude >= MAX_FORCE)
		{
			return Vector2.ClampMagnitude(force, MAX_FORCE);
		}

		// Flocking
		force += sepRule.GetForce(boid, FindNeighbors(boid)) * SEPARATION_WEIGHT;
		if (force.magnitude >= MAX_FORCE)
		{
			return Vector2.ClampMagnitude(force, MAX_FORCE);
		}
		force += aliRule.GetForce(boid, FindNeighbors(boid)) * ALIGNMENT_WEIGHT;
		if (force.magnitude >= MAX_FORCE)
		{
			return Vector2.ClampMagnitude(force, MAX_FORCE);
		}
		force += cohRule.GetForce(boid, FindNeighbors(boid)) * COHESION_WEIGHT;
		if (force.magnitude >= MAX_FORCE)
		{
			return Vector2.ClampMagnitude(force, MAX_FORCE);
		}

		return force;
	}

	void CollisionDetect(Boid boid)
	{
		Boid target = FindClosestOfType(boid, boid.preyType);
		if (target != null)
		{
			if (Distance(boid, target) < COLLISION_RADIUS)
			{
				// Collision has occurred! Convert target to the type of boid
				if (!convertedBoids.Contains(target))
				{
					convertedBoids.Add(target);
				}
			}
		}
	}

	public List<Boid> FindNeighbors(Boid target)
	{
		List<Boid> neighbors = new List<Boid>();

		foreach (Boid boid in boids)
		{
			if ((boid.type == target.type) && (Distance(target, boid) <= SENSE_RADIUS) && (boid != target))
			{
				neighbors.Add(boid);
			}
		}
		//Debug.Log(string.Format("{0} neighbors found.", neighbors.Count));
		return neighbors;
	}

	public List<Boid> FindNeighborsOfType(Boid target, BoidType type)
	{
		List<Boid> neighbors = new List<Boid>();

		foreach (Boid boid in boids)
		{
			if ((boid.type == type) && (Distance(target, boid) <= SENSE_RADIUS))
			{
				neighbors.Add(boid);
			}
		}
		return neighbors;
	}

	public Boid FindClosestOfType(Boid target, BoidType type)
	{
		Boid closestBoid = null;
		float closestDistance = SENSE_RADIUS;

		foreach (Boid boid in boids)
		{
			float distance = Distance(target, boid);
			if ((boid.type == type) && (distance < closestDistance))
			{
				closestBoid = boid;
				closestDistance = distance;
			}
		}
		return closestBoid;
	}

	float Distance(Boid b1, Boid b2)
	{
		return Vector2.Distance(b1.position, b2.position);
	}
}
