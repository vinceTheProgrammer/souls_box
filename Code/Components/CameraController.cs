using Sandbox;

namespace SoulsBox
{
	/// <summary>
	/// Controller for the player's camera
	/// </summary>
	[Title( "Souls Box Camera Controller" )]
	[Category( "Souls Box" )]
	[Icon( "video_camera_front" )]
	public sealed class CameraController : Component
	{

		[Property]
		public GameObject Camera {  get; set; }

		[Property]
		public GameObject CameraPivot { get; set; }

		/// <summary>
		/// X, Y sets distance & offset of camera. Z sets height via the CameraPivot.
		/// </summary>
		[Property]
		public Vector3 CameraOffset { get; set; }

		public GameObject CameraPivotGameObject;
		public Angles ForwardAngles;
		private Transform InitialCameraTransform;

		protected override void OnUpdate()
		{
			Vector3 _rotateAround = CameraPivot.Transform.Position;

			ForwardAngles = ForwardAngles.WithPitch( MathX.Clamp( ForwardAngles.pitch, -30.0f, 60.0f ) );

			InitialCameraTransform.Position = (_rotateAround + CameraOffset).WithZ(_rotateAround.z);

			Camera.Transform.World = InitialCameraTransform.RotateAround( _rotateAround, ForwardAngles );

			var cameraTrace = Scene.Trace.Ray( _rotateAround, Camera.Transform.World.Position ).Size( 5f ).WithoutTags( "player" ).Run();

			Camera.Transform.Position = cameraTrace.EndPosition;
		}

		protected override void OnStart()
		{
			InitialCameraTransform = Camera.Transform.World;
		}
	}
}
