using Sandbox;

public sealed class Respawn : Component, Component.ITriggerListener
{
	[Property] GameObject fatty { get; set; }
	[Property] GameObject spawnPoint { get; set; }


	void ITriggerListener.OnTriggerEnter( Collider other )
	{
		fatty.Transform.Position = spawnPoint.Transform.Position;
	}
}
