using UnityEngine;

namespace winterStage
{
    public abstract class Block : MonoBehaviour, IPoolable
    {
        public string blockTag;

        public SpriteRenderer[] _partsSprites;

        public SlashView _slashView;

        public Transform halvesParent;

        public Transform _leftSide;
        public Transform _rightSide;
        public Transform _leftHalf;
        public Transform _rightHalf;
        public Transform _leftShadow;
        public Transform _rightShadow;

        public MoveBlock mover;
        public RotateBlock rotator;
        public ScaleBlock scaler;

        [HideInInspector]public Vector3 currentDirection;
        [HideInInspector] public Vector3 currentScale;
        [HideInInspector] public float currentRotation;

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
