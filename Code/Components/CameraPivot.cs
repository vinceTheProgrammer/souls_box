using Sandbox;

namespace SoulsBox
{
	public sealed class CameraPivot : Component
	{
		[Property]
		public GameObject Player {  get; set; }

		protected override void OnFixedUpdate()
		{
			if ( Player.Components.TryGet( out SkinnedModelRenderer _renderer ) && Player.Components.TryGet(out CameraController _controller))
			{
				_renderer.CreateBoneObjects = true;
				GameObject _bone = _renderer.GetBoneObject( 0 );

				Transform.Position = Transform.Position.LerpTo(_bone.Transform.Position.WithZ( Player.Transform.Position.z + _controller.CameraOffset.z ), 0.2f);
			}
		}
	}
}
