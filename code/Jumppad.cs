using Sandbox;

public sealed class Jumppad : Component, Component.ITriggerListener
{
	[Property] public Rigidbody rb { get; set; }
	[Property] public float boost { get; set; }

	void ITriggerListener.OnTriggerEnter( Collider other )
	{
		rb.Velocity = rb.Velocity + Vector3.Up * boost;
	}

}
