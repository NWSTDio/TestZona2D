namespace GameMehanics.MoveInSlope {
	public class SlopeWalkState : SlopesStates { // ходьба по наклонной поверхности

		public SlopeWalkState(Player player, string nameState) : base(PlayerStates.SLOPE_WALK, player, nameState) { }

		public override void LogicUpdate() {
			base.LogicUpdate();

			if (IsExitingState)
				return;

			if (IsMove == false || CanWalkOnSlope == false) { // если прекратили движение или не можем передвигатся по поверности
				StateMachine.ChangeState(PlayerStates.SLOPE_IDLE);

				return;
				}

			if (RunButton) { // если начали бег
				StateMachine.ChangeState(PlayerStates.SLOPE_RUN);

				return;
				}

			float speed = Player.IsCrouch ? PlayerData.CrouchSpeed : PlayerData.WalkSpeed;

			Player.ChangeVelocity(speed * SlopeNormal.x * -InputX, speed * SlopeNormal.y * -InputX);
			}
		public override void PhysicsUpdate() {
			base.PhysicsUpdate();

			Player.ApplyFriction(CanWalkOnSlope && IsTouchWall == false ? PlayerData.NoFriction : PlayerData.FullFriction);
			}

		}
	}