using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidMgr : MonoBehaviour {
	List<Boid> boids = new List<Boid>();
	public Boid redBoidPrefab;
	public Boid blueBoidPrefab;
	public Boid greenBoidPrefab;
	int quantity = 10;
	float senseRadius = 40;
	float maxSpeed = 10;
	const int WIDTH = 100;
	const int HEIGHT = 100;

	// Use this for initialization
	void Start () {
		Debug.Log("Starting");
		Random.InitState(0);
		Quaternion rot = new Quaternion();
		for (int i = 0; i < quantity; i++)
		{
			Boid boidClone = Object.Instantiate<Boid>(redBoidPrefab, GetRandomPos(), rot);
			Debug.Log(string.Format("Drawing boid {0}", i));
			boidClone.Init(this, BoidType.RED, senseRadius, maxSpeed, GetRandomHeading()); 
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
		float x = Random.Range(-maxSpeed, maxSpeed);
		float y = Random.Range(-maxSpeed, maxSpeed);
		Vector2 heading = new Vector2(x, y);
		return Vector2.ClampMagnitude(heading, maxSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Boid boid in boids)
		{
			boid.MgrUpdate();
		}
	}

	public List<Boid> FindNeighbors(Boid target)
	{
		List<Boid> neighbors = new List<Boid>();

		foreach (Boid boid in boids)
		{
			if ((boid.type == target.type) && (Distance(target, boid) <= target.senseRadius) && (boid != target))
			{
				neighbors.Add(boid);
			}
		}
		Debug.Log(string.Format("{0} neighbors found.", neighbors.Count));
		return neighbors;
	}

	float Distance(Boid b1, Boid b2)
	{
		return Vector2.Distance(b1.position, b2.position);
	}
}
