using Sandbox;

public sealed class Lappetrigger : Component, Component.ITriggerListener
{
	[Property] int Amount { get; set; } = 1;

	public void OnTriggerEnter (Collider other )
	{
		var mayro = other.Components.Get<Kart>();
		if ( mayro != null )
		{
			mayro.lappe += Amount;
			mayro.MayroTalk.StartSound();
		}
	}

	public void OnTriggerExit (Collider other)
	{

	}

}
