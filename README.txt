Tyler Beaupre
Project 3: Boids of a Feather!!
https://youtu.be/AQQp5Hh8pqw

This is a standard Unity project which when played, shows a flocking simulation. There are three types, red squares, blue circles, and green triangles.
They chase each other around while avoiding the center obstacle. Reds chase Blues, Blues chase Greens, and Greens chase Reds.
If a boid is successfully caught, it is converted to match the type of its captor.

The object which spawns and controls the boids is called 'Spawner.'
The obstacle in the center of the screen is called 'Obstacle.'

Boid.cs - The script which represents a boid. On update, it truncates the force, then adds it to the heading and clamps that. Then the position is updated based on that heading.

BoidMgr.cs - The script which controls all of the boids. Handles all of the different forces which affect the boid and the weights associated with those forces.

BoidType.cs - RED, BLUE, and GREEN. An enum of the different types of boids.


Rule - The script which has all of the basic flocking functions like Seek, Flee, Pursue, and Evade.

FlockingRule - A special type of rule which calculates a force for a boid from a given set of neighbors.

Separation - The flocking rule which separates boids of the same type.

Alignment - The flocking rule which points the boids in the same direction.

Cohesion - The flocking rule which keeps the boids together.


Boundary - The script which keeps the boids inbounds.

Obstacle - The script which keeps the boids from running into the obstacle in the center of the screen.