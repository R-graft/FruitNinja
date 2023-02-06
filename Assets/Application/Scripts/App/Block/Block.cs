using UnityEngine;

namespace winterStage
{
    public abstract class Block : MonoBehaviour, IPoolable
    {
        public Transform halvesParent;

        public Transform _leftSide;
        public Transform _rightSide;

        public string tag;

        public MoveBlock mover;
        public RotateBlock rotator;
        public ScaleBlock scaler;

        public Vector3 currentDirection;

        public StateMashine StateMashine { get; set; }

        public void Init()
        {
            mover = new MoveBlock();
            rotator = new RotateBlock();
            scaler = new ScaleBlock();

            StateMashine = new StateMashine();
        }

        private void Update()
        {
            StateMashine.CurrentState.Update();
        }
    }

    public interface IBonusBlock
    {
        public void BonusEffect();
    }
}
