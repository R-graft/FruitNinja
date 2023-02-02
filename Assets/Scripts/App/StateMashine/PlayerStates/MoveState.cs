namespace winterStage
{
    public class MoveState : State
    {
        private Block _block;

        private MoveBlock _mover;

        private RotateBlock _rotator;

        private ScaleBlock _scaler;

        private FallChecker _faller;

        public MoveState(Block block)
        {
            _block = block;
            _mover = block.mover;
            _rotator = block.rotator;
            _scaler= block.scaler;
            _faller= block.faller;
        }
        public override void Enter()
        {
            _faller.GetFallValue();

            _mover.GetParabolaMoveDirections();

            _rotator.GetRandomRotateValue();

            _scaler.GetScaleValue();
        }

        public override void Update()
        {
            _faller.CheckFall();

            _mover.ParabolaMove();

            _rotator.Rotate(_block._sprite.transform);

            _rotator.Rotate(_block._shadow.transform);

            _scaler.RescaleBlock();
        }
    }
}
