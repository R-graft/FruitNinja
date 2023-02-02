namespace winterStage
{
    public class MoveState : State
    {
        private Block _block;

        private MoveBlock _mover;

        private RotateBlock _rotator;

        private ScaleBlock _scaler;

        public MoveState(Block block)
        {
            _block = block;
            _mover = block.mover;
            _rotator = block.rotator;
            _scaler= block.scaler;
        }
        public override void Enter()
        {
            _mover.GetParabolaMoveDirections();

            _rotator.GetRandomRotateValue();

            _scaler.GetScaleValue();
        }

        public override void Update()
        {
            _mover.ParabolaMove();

            _rotator.Rotate(_block._sprite.transform);

            _rotator.Rotate(_block._shadow.transform);

            _scaler.RescaleBlock();
        }
    }
}
