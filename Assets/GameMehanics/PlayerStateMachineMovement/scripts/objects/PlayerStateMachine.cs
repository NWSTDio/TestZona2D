namespace GameMehanics.PlayerStateMachineMovement {
	public class PlayerStateMachine { // машина состояний

		private PlayerState _currentState;// текущее состояние

		public void Initialize(PlayerState state) { // первая инициализация состояния
			_currentState = state;

			_currentState.Enter();
			}

		public void ChangeState(PlayerState state) { // смена состояния
			_currentState?.Exit();

			_currentState = state;

			_currentState.Enter();
			}

		public void LogicUpdate() => _currentState?.LogicUpdate();// обновление логики состояния
		public void PhysicsUpdate() => _currentState?.PhysicsUpdate();// обновление физики состояния

		}
	}