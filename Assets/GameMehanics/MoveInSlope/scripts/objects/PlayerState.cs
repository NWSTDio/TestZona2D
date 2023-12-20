using UnityEngine;

namespace GameMehanics.MoveInSlope {
	public abstract class PlayerState<PlayerStates> {

		public PlayerStates ID { get; private set; }

		protected Player Player;// игрок
		protected PlayerData PlayerData;// данные игрока
		protected PlayerStateMachine StateMachine;
		protected string NameState;// имя состояние
		protected float StartTime;// время вхождения игрока в состояние
		protected bool IsExitingState;// выход из главного состояния (нужно для подсостояний)

		public PlayerState(PlayerStates id, Player player, string nameState) {
			ID = id;

			Player = player;
			NameState = nameState;

			PlayerData = Player.PlayerData;
			StateMachine = Player.StateMachine;
			}

		public virtual void Enter() {
			MenuUI.Instance.UpdateState(NameState);

			if (Application.platform != RuntimePlatform.WindowsEditor)
				Debug.Log("Enter: " + NameState);

			StartTime = Time.time;

			PhysicsUpdate();

			IsExitingState = false;

			Player.Animations.SetBool(NameState, true);
			}
		public virtual void LogicUpdate() { }
		public virtual void PhysicsUpdate() { }
		public virtual void Exit() {
			MenuUI.Instance.UpdatePrevState(NameState);

			if (Application.platform != RuntimePlatform.WindowsEditor)
				Debug.Log("Exit: " + NameState);

			IsExitingState = true;

			Player.Animations.SetBool(NameState, false);
			}
		}
	}