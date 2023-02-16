using UnityEngine;

namespace winterStage
{
    public class Block : MonoBehaviour, IPoolable
    {
        public string blockTag;

        public SpriteRenderer[] partsRenderers;

        public Transform main;
        public Transform shadow;

        public MoveBlock mover;
        public RotateBlock rotator;
        public ScaleBlock scaler;

        [HideInInspector] public SplashView slashView;

        [HideInInspector] public Vector3 currentDirection;
        [HideInInspector] public Vector3 currentScale;
        [HideInInspector] public float currentRotation;

        private readonly Vector3 ShadowOffset = new Vector3(0.4f, -0.4f, 0);

        protected readonly Vector3 DefaultScale = Vector3.one;
        protected readonly Vector2 DefaultPos = Vector3.zero;
        protected readonly Quaternion DefaultRot = Quaternion.identity;

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

        public virtual void SlashUpdateBehaviour()
        {
        }
        public virtual void SlashInBehaviour()
        {
        }
        public virtual void SetDefaultTransform()
        {
            transform.localPosition = DefaultPos;
            transform.localScale = DefaultScale;

            main.localPosition = DefaultPos;
            main.localRotation = DefaultRot;

            shadow.localPosition = ShadowOffset;
            shadow.localRotation = DefaultRot;
        }
    }

    public interface IBonusBlock
    {
        public void BonusEffect();
    }
}
