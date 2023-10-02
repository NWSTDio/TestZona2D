using System;
using UnityEngine;

namespace NWSTDio.BezierExample {
	public class GameController : MonoBehaviour {

		public static event EventHandler OnClickRestart;

		private void Update() {
			if (Input.GetKeyUp(KeyCode.Space))
				OnClickRestart?.Invoke(this, EventArgs.Empty);
			}

		}
	}