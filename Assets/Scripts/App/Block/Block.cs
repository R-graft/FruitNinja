using UnityEngine;

namespace winterStage
{
    public abstract class Block : MonoBehaviour, IPoolable
    {
        public string blockTag;

        [SerializeField] private SpriteRenderer _renderer;

        public MoveBlock _mover;
        public RotateBlock _rotator;
        public ScaleBlock _scaler;

        public StateMashine StateMashine { get; set; }

        private void Init()
        {
            _mover ??= new MoveBlock(transform);

            _rotator ??= new RotateBlock(transform);

            _scaler ??= new ScaleBlock(transform);

            StateMashine ??= new StateMashine();

            StateMashine.Init(new MoveState(_mover, _rotator, _scaler));
        }
    
        private void Update()
        {
            //StateMashine.CurrentState.Update();
        }

        private void FixedUpdate()
        {
            //StateMashine.CurrentState.FixedUpdate();
        }

        public virtual void PoolOnGet(Block block)
        {
            //Init();

            //block.gameObject.SetActive(true);
        }

        public virtual void PoolOnCreate(Block block)
        {
            //block.gameObject.SetActive(false);
        }

        public virtual void PoolOnDisable(Block block)
        {
            //block.gameObject.SetActive(false);
        }
    }

    public interface IBonusBlock
    {
        public void BonusEffect();
    }
}
