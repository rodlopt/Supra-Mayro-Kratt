using Sandbox;
using System;
using System.Security;

public sealed class Kart : Component
{
	[Property] public float maxSpeed { get; set; }
	[Property] public float acceleration { get; set; }
	[Property] public float rotationSpeed { get; set; }
	[Property] public float smoothingSpeed { get; set; }
	[Property] public float minimumPitch { get; set; }


	[Property] public SkinnedModelRenderer mayro { get; set; }
	[Property] public SoundPointComponent brrr { get; set; }
	[Property] public Rigidbody rb { get; set; }
	[Property] public MayroAnimationHelper AnimationHelper { get; set; }

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
			AnimationHelper.TiltLevel = -1f;
		}
		else if ( Input.Down( "Right" ) )
		{
			rot *= Rotation.From( 0, Time.Delta * -90.0f, 0 ) * rotationSpeed;
			AnimationHelper.TiltLevel = 1f;
		}
		else
		{
			AnimationHelper.TiltLevel = MathX.Lerp( AnimationHelper.TiltLevel, 0f, Time.Delta * smoothingSpeed );
		}

		if ( Input.Down( "Jump" ) )
		{
			//rb.Velocity = rb.Velocity + Vector3.Up * 64;
			rb.Velocity = 0.0f;
		}

		Transform.Local = new Transform( pos, rot, 1 );

	}
}
