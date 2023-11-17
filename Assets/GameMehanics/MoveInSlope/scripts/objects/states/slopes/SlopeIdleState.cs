namespace GameMehanics.MoveInSlope {
	public class SlopeIdleState : SlopesStates { // стоим на наклонной поверхности

		public SlopeIdleState(Player player, string nameState) : base(PlayerStates.SLOPE_IDLE, player, nameState) { }

		public override void Enter() {
			base.Enter();

			Player.FreezeVelocity();// заморозим скорость
			}
		public override void LogicUpdate() {
			base.LogicUpdate();

			if (IsExitingState)
				return;

			if (IsMove && CanWalkOnSlope) { // если движемся и можем двигатся по наклонной поверхности
				StateMachine.ChangeState(RunButton ? PlayerStates.SLOPE_RUN : PlayerStates.SLOPE_WALK);

				return;
				}

			if (IsTouchWall)
				MenuUI.Instance.UpdateKeysInfo("Нажмите [Q] чтобы поднятся по стене");
			else
				MenuUI.Instance.ClearKeysInfo();
			}
		public override void PhysicsUpdate() {
			base.PhysicsUpdate();

			Player.ApplyFriction(CanWalkOnSlope == false ? PlayerData.NoFriction : PlayerData.FullFriction);
			}

		}
	}