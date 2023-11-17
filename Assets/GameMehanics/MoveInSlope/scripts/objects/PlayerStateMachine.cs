using System.Collections.Generic;

namespace GameMehanics.MoveInSlope {
	public class PlayerStateMachine {

		public PlayerState<PlayerStates> CurrentState { get; private set; }// текущее состояние игры

		private readonly Dictionary<PlayerStates, PlayerState<PlayerStates>> _states;// все состояния игры

		public PlayerStateMachine() {
			_states = new Dictionary<PlayerStates, PlayerState<PlayerStates>>();
			}

		public void AddState(PlayerState<PlayerStates> state) => _states.Add(state.ID, state);// добавить состояние
		public PlayerState<PlayerStates> GetState(PlayerStates stateID) => _states[stateID];

		public void ChangeState(PlayerStates stateID) => SetCurrentState(_states[stateID]); // установить текущее состояние

		public void LogicUpdate() => CurrentState?.LogicUpdate();// обновление логики состояния
		public void PhysicsUpdate() => CurrentState?.PhysicsUpdate();// обновление физики состояния

		private void SetCurrentState(PlayerState<PlayerStates> state) { // установить новое состояние игры
			if (CurrentState == state) // если состояние не изменилось
				return;

			CurrentState?.Exit();// выход из текущего состояние
			CurrentState = state;// установим состояние
			CurrentState?.Enter();// вход в новое состояние
			}

		}
	}