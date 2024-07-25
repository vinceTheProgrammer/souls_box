using Sandbox;

namespace SoulsBox
{
	/// <summary>
	/// Vitals for souls box character
	/// </summary>
	[Title( "Souls Box Character Vitals" )]
	[Category( "Souls Box Character" )]
	[Icon( "favorite" )]
	public sealed class CharacterVitals : Component
	{
		[Property]
		public int MaxHealth { get; set; }

		[Property]
		public int MaxStamina { get; set; }

		public int Health { get; private set; }

		public int Stamina { get; private set; }

		protected override void OnStart()
		{
			Health = MaxHealth;
			Stamina = MaxStamina;
		}
	}
}
