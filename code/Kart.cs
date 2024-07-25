using Sandbox;
using System;
using System.Security;

public sealed class Kart : Component
{
	[Property] public float maxSpeed;
	[Property] public float acceleration;
	[Property] public float rotationSpeed;
	[Property] public float minimumPitch;

	[Property] public SoundPointComponent brrr;

	[Property] public Rigidbody rb;

	protected override void OnUpdate()
	{

		brrr.Pitch = MathF.Max( 0.001f * rb.Velocity.Length, minimumPitch );
	}
	protected override void OnFixedUpdate()
	{
		var rot = GameObject.Transform.Rotation;
		var pos = GameObject.Transform.Position;

		if ( Input.Down( "Forward" ) )
		{
			rb.Velocity = rb.Velocity + Transform.Local.Forward * acceleration;
		}

		if ( Input.Down( "Backward" ) )
		{
			rb.Velocity = rb.Velocity + Transform.Local.Backward * acceleration;
		}

		if ( Input.Down( "Left" ) )
		{
			rot *= Rotation.From( 0, Time.Delta * 90.0f, 0 ) * rotationSpeed;
		}

		if ( Input.Down( "Right" ) )
		{
			rot *= Rotation.From( 0, Time.Delta * -90.0f, 0 ) * rotationSpeed;
		}

		if ( Input.Down( "Jump" ) )
		{
			//rb.Velocity = rb.Velocity + Vector3.Up * 64;
			rb.Velocity = 0.0f;
		}

		Transform.Local = new Transform( pos, rot, 1 );

	}
}
