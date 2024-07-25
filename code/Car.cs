using Sandbox;
using System.Security;

public sealed class Car : Component
{
	[Property] public float acceleration = 10f;

	[Property] public float currentSpeed = 0.0f;

	protected override void OnUpdate()
	{
		// drive tank
		Vector3 movement = 0;
		if ( Input.Down( "Forward" ) ) movement += Transform.World.Forward;
		if ( Input.Down( "backward" ) ) movement += Transform.World.Backward;

		if ( Input.Down( "Forward" ) )
		{
			currentSpeed += acceleration * Time.Delta;
		}
		else
		{

			currentSpeed = 0.0f;
			
		}

		var rot = GameObject.Transform.Rotation;
		var pos = GameObject.Transform.Position + movement * Time.Delta * currentSpeed;

		if ( Input.Down( "Left" ) )
		{
			rot *= Rotation.From( 0, Time.Delta * 90.0f, 0 );
		}

		if ( Input.Down( "Right" ) )
		{
			rot *= Rotation.From( 0, Time.Delta * -90.0f, 0 );
		}

		if ( Input.Down( "Jump" ) )
		{
			currentSpeed = 0.0f;
		}

		Transform.Local = new Transform( pos, rot, 1 );

	}
}
