namespace GameMehanics.MoveInSlope {
	public class SlopeRunState : SlopesStates { // бег по наклонной поверхности

		public SlopeRunState(Player player, string nameState) : base(PlayerStates.SLOPE_RUN, player, nameState) { }

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (IsExitingState)
				return;

			if (IsMove == false || CanWalkOnSlope == false) { // если прекратили движение или не можем передвигатся по поверности
				StateMachine.ChangeState(PlayerStates.SLOPE_IDLE);

				return;
				}

			if (RunButton == false) { // если прекратили бег
				StateMachine.ChangeState(PlayerStates.SLOPE_WALK);

				return;
				}

			float speed = Player.IsCrouch ? PlayerData.CrouchSpeed : PlayerData.RunSpeed;

			Player.ChangeVelocity(speed * SlopeNormal.x * -InputX, speed * SlopeNormal.y * -InputX);
			}

		public override void PhysicsUpdate() {
			base.PhysicsUpdate();

			Player.ApplyFriction(CanWalkOnSlope && IsTouchWall == false ? PlayerData.NoFriction : PlayerData.FullFriction);
			}

		}
	}