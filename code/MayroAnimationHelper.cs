using Sandbox;

public sealed class MayroAnimationHelper : Component, Component.ExecuteInEditor
{
	[Property] public SkinnedModelRenderer Target { get; set; }
	public float TiltLevel
	{
		get => Target.GetFloat( "tilt" );
		set => Target.Set("tilt", value );
	}


}