using Sandbox;
using Sandbox.Citizen;
using System;

namespace SoulsBox 
{
	/// <summary>
	/// Interface between s&box character controller and souls box agent
	/// </summary>
	[Title( "Souls Box Character Movement Controller" )]
	[Category( "Souls Box Character" )]
	[Icon( "directions_run" )]
	public sealed class CharacterMovementController : Component
	{
		// TEMPORARY
		private bool isRolling;
		private bool midwayPoint;

		/// <summary>
		/// Character walk speed
		/// </summary>
		[Property]
		public float WalkSpeed { get; set; }

		/// <summary>
		/// Character run speed
		/// </summary>
		[Property]
		public float RunSpeed { get; set; }

		/// <summary>
		/// Upward force to be applied when jumping TODO: make animation driven
		/// </summary>
		[Property]
		public float JumpForce { get; set; }

		/// <summary>
		/// Backward force to be applied when backstepping TODO: make animation driven
		/// </summary>
		[Property]
		public float BackstepForce { get; set; }

		[Property]
		public CharacterController CharacterController { get; set; }

		[Property]
		public CharacterAgent agent { get; set; }

		[Property]
		public CitizenAnimationHelper AnimationHelper { get; set; }

		protected override void OnUpdate()
		{

			//Log.Info( isRolling );

			if (isRolling)
			{
				Vector3 _currentVelocity = CharacterController.Velocity;
				Vector3 _targetVelocity = agent.Transform.Rotation.Forward * 200.0f;
				float _targetSpeed = agent.IsRunActive() && !agent.IsGuardActive() ? RunSpeed : WalkSpeed;
				Vector3 _targetLerpVelocity = agent.GetMoveVector().Normal * _targetSpeed;
				if (midwayPoint == true )
				{
					Log.Info( "Current Velocity: " + _currentVelocity );
					Log.Info( "Target Lerp Velocity: " + _targetLerpVelocity );
					_targetVelocity = _currentVelocity.LerpTo( _targetLerpVelocity, 0.05f, true);
					Log.Info( "Interpolated Target Velocity: " + _targetVelocity );
				}
				CharacterController.Velocity = _targetVelocity;
				CharacterController.Move();
			} else
			{
				//Log.Info( "!!!!!" );
				float _targetSpeed = agent.IsRunActive() && !agent.IsGuardActive() ? RunSpeed : WalkSpeed;
				Vector3 _targetVelocity = agent.GetMoveVector().Normal * _targetSpeed;

				CharacterController.Accelerate( _targetVelocity );
				CharacterController.Acceleration = 10.0f;
				CharacterController.ApplyFriction( 5.0f );
				CharacterController.Move();

				// TODO put this in CharacterAnimationController
				if ( AnimationHelper != null )
				{
					AnimationHelper.IsGrounded = CharacterController.IsOnGround;
					AnimationHelper.WithVelocity( CharacterController.Velocity );
					AnimationHelper.WithLook( _targetVelocity );
					if ( agent.roll == true )
					{
						AnimationHelper.Target.Set( "roll_forward", true );
					}
				}
			}
		}

		protected override void OnStart()
		{
			AnimationHelper.Target.OnGenericEvent = ( SceneModel.GenericEvent genericEvent ) =>
			{
				//Log.Info( genericEvent.String );
				switch ( genericEvent.String )
				{
					case "roll_start":
						Log.Info( "setting to true" );
						isRolling = true;
						break;
					case "roll_end":
						Log.Info( "setting to false" );
						isRolling = false;
						agent.roll = false;
						midwayPoint = false;
						break;
					case "roll_midway":
						Log.Info( "mid" );
						midwayPoint = true;
						break;
				}
			};
		}
	}
}
