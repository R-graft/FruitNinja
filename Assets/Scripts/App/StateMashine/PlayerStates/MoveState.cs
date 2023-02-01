namespace winterStage
{
    public class MoveState : State
    {
        public MoveBlock _mover;

        public RotateBlock _rotator;

        public ScaleBlock _scaler;

        public MoveState(MoveBlock mover, RotateBlock rotator, ScaleBlock scaler)
        {
            _mover = mover;
            _rotator = rotator;
            _scaler= scaler;
        }
        public override void Enter()
        {
            _mover.GetParabolaMoveDirections();

            _rotator.GetRotateValue();

            _scaler.GetScaleValue();
        }

        public override void FixedUpdate()
        {
            _mover.ParabolaMove();

            _rotator.Rotate();

            _scaler.RescaleBlock();
        }
    }
}
