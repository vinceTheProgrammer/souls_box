using Sandbox;

namespace SoulsBox
{
	public sealed class CharacterInputManager : Component
	{

		[Property]
		public CharacterAgent agent {  get; set; }

		// held states
		public bool Run;
		public bool Guard;



		protected override void OnFixedUpdate()
		{
			Run = Input.Down("Run") ? true : false;
			Guard = Input.Down( "Guard" ) ? true : false;
		}
	}
}
