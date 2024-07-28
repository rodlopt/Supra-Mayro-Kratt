using Sandbox;
using System;
using System.Security;

public sealed class Kart : Component
{
	[Property] public int lappe { get; set; } = 1;
	[Property] public CameraComponent Camera { get; set; }
	[Property] public CameraComponent WinCamera { get; set; }
	[Property] public Lappe uishit { get; set; }
	[Property] public Yuwienner uishitner { get; set; }

	[Property] public float maxSpeed { get; set; }
	[Property] public float acceleration { get; set; }
	[Property] public float rotationSpeed { get; set; }
	[Property] public float smoothingSpeed { get; set; }
	[Property] public float minimumPitch { get; set; }

	[Property] public SoundPointComponent MayroTalk { get; set; }
	[Property] public SoundPointComponent brrr { get; set; }
	[Property] public Rigidbody rb { get; set; }
	[Property] public MayroAnimationHelper AnimationHelper { get; set; }
	[Property] public SkinnedModelRenderer mayroModel { get; set; }

	[Property] public bool Kratt { get; set; }

	[Property] public bool AI { get; set; }
	[Property] public float AIspeed { get; set; }
	[Property] public int currentWaypoint { get; set; }
	[Property] List<GameObject> AIwaypoints { get; set; }

	protected override void OnStart()
	{
		Kratt = true;
		Camera.Enabled = true;
		WinCamera.Enabled = false;
		uishit.Enabled = true;
		uishitner.Enabled = false;
		AI = false;
		AnimationHelper.Win = false;
	}

	protected override void OnUpdate()
	{
		if ( AI )
		{
			Vector3 direction = (AIwaypoints[currentWaypoint].Transform.Position - Transform.Position).Normal;

			Vector3 newPos = Transform.Position + direction * AIspeed * Time.Delta;

			float distanceToCurrentWaypoint = Vector3.DistanceBetween( Transform.Position, AIwaypoints[currentWaypoint].Transform.Position );

			if ( Vector3.DistanceBetween( newPos, AIwaypoints[currentWaypoint].Transform.Position ) < distanceToCurrentWaypoint )
			{
				Transform.Position = newPos;
			}
			else
			{
				Transform.Position = AIwaypoints[currentWaypoint].Transform.Position;
				Transform.Rotation = AIwaypoints[currentWaypoint].Transform.Rotation;

				if ( currentWaypoint < AIwaypoints.Count - 1 )
				{
					currentWaypoint++;
				}
				else
				{
					currentWaypoint = 0;
				}
			}

			Vector3 nextDir = (AIwaypoints[currentWaypoint].Transform.Position - Transform.Position).Normal;
			Transform.Rotation = Rotation.LookAt( nextDir );

		}

		if (lappe == 4 )
		{
			Kratt = false;
			Camera.Enabled = false;
			WinCamera.Enabled = true;
			uishit.Enabled = false;
			uishitner.Enabled = true;
			AI = true;
			AnimationHelper.Win = true;
		}

		brrr.Pitch = MathF.Max( 0.001f * rb.Velocity.Length, minimumPitch );

		
	}
	protected override void OnFixedUpdate()
	{
		if ( Kratt )
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
}
