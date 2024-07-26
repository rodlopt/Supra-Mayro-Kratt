using Sandbox;

public sealed class Die : Component, Component.ICollisionListener
{
	[Property] GameObject hi { get; set; }
	void ICollisionListener.OnCollisionStart(Sandbox.Collision collision) 
	{
		GameObject.Destroy();
	}

}
