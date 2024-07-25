using Sandbox;

namespace SoulsBox
{
	public abstract class CharacterAgent : Component
	{
		public bool roll { get; set; }

		public abstract bool IsRunActive();

		public abstract bool IsGuardActive();

		public abstract Vector3 GetMoveVector();
	}
}
