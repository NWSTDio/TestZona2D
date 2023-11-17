using UnityEngine;

namespace GameMehanics.MoveInSlope {
    public class LedgeClimbState : PlayerState<PlayerStates> {

        private Vector2 _detectedPosition, _startPosition, _finishPosition;// позиция детекта уступа
        private float _timer;// таймер для заморозки вдижения (вместо анимации)
        private int _inputX, _inputY;// ввод по оси X и Y
        private bool _isClimbing;// начал ли подьем на уступ
        private bool _isTouchCeiling;

        public LedgeClimbState(Player player, string nameState) : base(PlayerStates.LEDGE_CLIMB, player, nameState) { }

        public override void Enter() {
            base.Enter();

            Player.SetPosition(_detectedPosition);

            Vector2 corner = Player.DetermineCornerPosition();// получим угол уступа

            _startPosition = new Vector2() {
                x = corner.x - (Player.FacingDirection * PlayerData.StartOffset.x),
                y = corner.y - PlayerData.StartOffset.y
                };// начальная позиция игрока перед уступом

            _finishPosition = new Vector2() {
                x = corner.x + (Player.FacingDirection * PlayerData.FinishOffset.x),
                y = corner.y + PlayerData.FinishOffset.y
                };// конечная позиция игрока на уступе

            _timer = .5f;
            _isClimbing = false;
            _isTouchCeiling = CheckForSpace(corner);// проверка нет ли потолка на вершине уступа

            HoldPosition();// заморозим позицию

            if (_isTouchCeiling) // если все таки есть уступ, то сметсим ниже конечную позицию игрока
                _finishPosition.y -= .5f;

            MenuUI.Instance.UpdateKeysInfo("Нажмите " + (Player.FacingDirection == 1 ? "D" : "A") + " чтобы вскарабкатся [S] чтобы упасть");
            }

        public override void LogicUpdate() {
            base.LogicUpdate();

            _inputX = Player.InputHandler.Movement.x;
            _inputY = Player.InputHandler.Movement.y;

            HoldPosition();// заморозим позицию

            if (_timer <= 0) {
                if (_isClimbing) { // если подьем
                    if (_isTouchCeiling) // если на финишной точке есть потолок
                        Player.StartCrouch();// включим режим приседания

                    StateMachine.ChangeState(PlayerStates.IDLE);

                    return;
                    }

                if (_inputX == Player.FacingDirection && _isClimbing == false) { // если движемся в сторону взгляда и не было подьема
                    _isClimbing = true;// начнем подьем

                    _timer = .5f;
                    }
                else if (_inputY == -1 && _isClimbing == false) { // если нажали спуск и нет режима подьема
                    StateMachine.ChangeState(PlayerStates.FLY);

                    return;
                    }
                }
            else
                _timer -= Time.deltaTime;
            }

        public override void Exit() {
            base.Exit();

            if (_isClimbing) // если был подьем
                Player.SetPosition(_finishPosition);// переместим игрока

            MenuUI.Instance.ClearKeysInfo();
            }

        public void SetDetectedPosition(Vector3 position) => _detectedPosition = position;// получение позиции в которой уступ закончился

        private void HoldPosition() {
            Player.ChangeVelocity(0, 0);
            Player.SetPosition(_startPosition);
            }
        private bool CheckForSpace(Vector2 corner) => Physics2D.Raycast(corner + Vector2.up * .015f + (Vector2.right * Player.FacingDirection * .5f), Vector2.up, 1f, PlayerData.WhatIsGround);// проверка есть ли пустое пространство в финишной позиции

        }
    }