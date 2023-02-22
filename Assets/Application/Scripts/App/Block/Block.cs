using System;
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

        protected Vector3 DefaultScale = Vector3.one;
        protected Vector3 DefaultPos = Vector3.zero;
        protected Quaternion DefaultRot = Quaternion.identity;

        public bool isBonus;

        public bool is3d;
        public StateMashine StateMashine { get; set; }

        public virtual void Init()
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
        public virtual void ActiveUpdateBehaviour()
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

    public interface IBoostBlock
    {
        public BonusController BonusController { get; set; }

        public void BonusEffect();
    }
}
