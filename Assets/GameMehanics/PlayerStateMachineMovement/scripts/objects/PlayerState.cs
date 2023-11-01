using UnityEngine;

namespace GameMehanics.PlayerStateMachineMovement {
	public abstract class PlayerState { // основной класс состояния игрока

		protected Player Player;// игрок
		protected PlayerStateMachine StateMachine;// машина состояний
		protected PlayerData PlayerData;// данные игрока
		protected string NameState;// имя состояние
		protected float StartTime;// время вхождения игрока в состояние
		protected bool IsExitingState;// выход из главного состояния (нужно для подсостояний)

		public PlayerState(Player player, string nameState) {
			Player = player;
			NameState = nameState;

			StateMachine = Player.StateMachine;
			PlayerData = Player.PlayerData;
			}

		public virtual void Enter() { // вхождение в состояние
			Debug.Log("Enter " + NameState);

			StartTime = Time.time;// время вхождение в состояние

			DoChecks();// проверка состояний взаимодействий с окружающей средой

			IsExitingState = false;// отключить прерывание состояния
			}
		public virtual void LogicUpdate() { } // логика работы состояния
		public virtual void PhysicsUpdate() => DoChecks();// обновление физики
		public virtual void Exit() {
			Debug.Log("Exit " + NameState);

			IsExitingState = true;// включить прерывание состояния
			} // выход из состояния
		public virtual void DoChecks() { } // проверка состояний взаимодействий с окружающей средой

		}
	}