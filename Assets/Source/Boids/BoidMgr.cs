﻿using System.Collections;
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
	const float MAX_SPEED = 1f;
	const float MAX_FORCE = 1.5f;

	// Flocking rule set
	const float SEPARATION_WEIGHT = 0.8f;
	const float ALIGNMENT_WEIGHT = 0.3f;
	const float COHESION_WEIGHT = 0.2f;
	Separation sepRule = new Separation();
	Alignment aliRule = new Alignment();
	Cohesion cohRule = new Cohesion();

	const float BOUNDARY_WEIGHT = 2f;
	Boundary boundary = new Boundary(WIDTH, HEIGHT, MAX_SPEED * 2);

	List<Boid> boids = new List<Boid>(); // updated list of boids

	// Use this for initialization
	void Start () {
		Random.InitState(0);
		CreateBoids(BoidType.RED, QUANTITY);
		CreateBoids(BoidType.BLUE, QUANTITY);
		CreateBoids(BoidType.GREEN, QUANTITY);
	}

	void CreateBoids (BoidType type, int quantity)
	{
		Boid boidPrefab;
		switch(type)
		{
		case(BoidType.BLUE):
			boidPrefab = blueBoidPrefab;
			break;
		case(BoidType.GREEN):
			boidPrefab = greenBoidPrefab;
			break;
		case(BoidType.RED):
			boidPrefab = redBoidPrefab;
			break;
		default:
			boidPrefab = blueBoidPrefab;
			break;
		}

		Quaternion rot = new Quaternion();
		for (int i = 0; i < quantity; i++)
		{
			Boid boidClone = Object.Instantiate<Boid>(boidPrefab, GetRandomPos(), rot);
			Debug.Log(string.Format("Drawing boid {0}", i));
			boidClone.Init(type, MAX_SPEED, MAX_FORCE, GetRandomHeading()); 
			boids.Add(boidClone);
		}
	}

	Vector2 GetRandomPos()
	{
		int x = Random.Range(-WIDTH, WIDTH);
		int y = Random.Range(-HEIGHT, HEIGHT);
		return new Vector2(x, y);
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
		}
	}

	Vector2 GetBoidForce(Boid boid)
	{
		Vector2 force = boundary.GetForce(boid) * BOUNDARY_WEIGHT;
		if (force.magnitude >= MAX_FORCE)
		{
			Debug.Log("Approaching Wall!");
			return Vector2.ClampMagnitude(force, MAX_FORCE);
		}

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

		// Add in attraction and repulsion to other types
		Debug.Log("Not full force");
		return force;
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

	float Distance(Boid b1, Boid b2)
	{
		return Vector2.Distance(b1.position, b2.position);
	}
}
