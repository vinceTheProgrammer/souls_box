using Sandbox;
using Sandbox.Diagnostics;

namespace SoulsBox
{
	/// <summary>
	/// Interface between Souls Box character and an IRL human agent.
	/// </summary>
	[Title( "Souls Box Player Agent" )]
	[Category( "Souls Box" )]
	[Icon( "man" )]
	public sealed class AgentPlayer : CharacterAgent
	{

		[Property]
		public CameraController CameraController { get; set; }

		[Property]
		public GameObject Camera { get; set; }

		private Rotation LastMoveDirectionRotation;

		public override Vector3 GetMoveVector()
		{
			return Input.AnalogMove * Camera.Transform.Rotation;
		}

		public override bool IsGuardActive()
		{
			return Input.Down( "Guard" );
		}

		public override bool IsRunActive()
		{
			if (Input.Down("Run_c"))
			{
				Log.Info( "Run input pressed" );
			}
			return Input.Down( "Run_c" );
		}

		protected override void OnUpdate()
		{
			CameraController.ForwardAngles += Input.AnalogLook;

			if (roll != true)
			{
				if ( GetMoveVector().Length > 0 ) LastMoveDirectionRotation = Rotation.FromYaw( (GetMoveVector()).EulerAngles.yaw );
				Transform.Rotation = Rotation.Lerp( Transform.Rotation, LastMoveDirectionRotation, 0.1f );
			}
			
			if (Input.Pressed("Roll"))
			{
				roll = true;
			}
		}


	}
}

