using Sandbox;

public sealed class Test : Component
{
	[Property] public float speed = 5.0f;
	[Property] public Rigidbody rb;
	protected override void OnUpdate()
	{
		
	}
	protected override void OnFixedUpdate()
	{
		rb.Velocity = Transform.Local.Right * speed;
	}
}
