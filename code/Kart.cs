using Sandbox;

public sealed class Kart : Component
{
	[Property] public float maxSpeed;
	[Property] public float acceleration;
	[Property] public float rotationSpeed;

	[Property] public Rigidbody rb;

	protected override void OnUpdate()
	{

	}
	protected override void OnFixedUpdate()
	{
		if ( Input.Down( "Jump" ) )
		{
			rb.Velocity = rb.Velocity + Vector3.Up * 64;
		}
	}
}
