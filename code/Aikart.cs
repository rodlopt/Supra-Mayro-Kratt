using Sandbox;
using System.Numerics;

public sealed class Aikart : Component
{
	[Property] public bool AI { get; set; }
	[Property] public float AIspeed { get; set; }
	[Property] public int currentWaypoint { get; set; }
	[Property] List<GameObject> AIwaypoints { get; set; }

	protected override void OnUpdate()
	{
		if ( AI )
		{
			Vector3 direction = (AIwaypoints[currentWaypoint].Transform.Position - Transform.Position).Normal;

			Vector3 newPos = Transform.Position + direction * AIspeed * Time.Delta;

			float distanceToCurrentWaypoint = Vector3.DistanceBetween( Transform.Position, AIwaypoints[currentWaypoint].Transform.Position );

			if ( Vector3.DistanceBetween( newPos, AIwaypoints[currentWaypoint].Transform.Position) < distanceToCurrentWaypoint )
			{
				Transform.Position = newPos;
			}
			else
			{
				Transform.Position = AIwaypoints[currentWaypoint].Transform.Position;
				Transform.Rotation = AIwaypoints[currentWaypoint].Transform.Rotation;

				if(currentWaypoint < AIwaypoints.Count - 1)
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

		
	}
}
