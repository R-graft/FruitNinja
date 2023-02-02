using System.Collections;
using UnityEngine;

namespace winterStage
{
    public abstract class Block : MonoBehaviour, IPoolable
    {
        public GameObject _sprite;

        public GameObject _shadow;

        public string tag;

        public MoveBlock mover;
        public RotateBlock rotator;
        public ScaleBlock scaler;
        public FallChecker faller;

        public StateMashine StateMashine { get; set; }

        public void Init()
        {
            mover ??= new MoveBlock(transform);
            rotator ??= new RotateBlock();
            scaler ??= new ScaleBlock(transform);
            faller ??= new FallChecker(this);

            StateMashine ??= new StateMashine();

            StateMashine.Init(new MoveState(this));

            StartCoroutine(Updater());
        }

        private IEnumerator Updater()
        {
            while (true)
            {
                StateMashine.CurrentState.Update();

                yield return null;
            }
        }
    }

    public interface IBonusBlock
    {
        public void BonusEffect();
    }
}
