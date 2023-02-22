using UnityEngine;

namespace winterStage
{
    public class Block3D : Block
    {
        public Transform mainLeft;
        public Transform mainRight;

        private Quaternion _mainLeftRotation;
        private Quaternion _mainRightRotation;

        private Vector3 _mainLeftScale;
        private Vector3 _mainRightScale;

        private Vector3 _initPosition;

        public override void Init()
        {
            base.Init();

            _mainLeftRotation = mainLeft.transform.localRotation;
            _mainRightRotation = mainRight.transform.localRotation;

            _mainLeftScale = mainLeft.transform.localScale;
            _mainRightScale = mainRight.transform.localScale;

            _initPosition = main.transform.localPosition;
        }
        public override void ActiveUpdateBehaviour()
        {
            rotator.FullRotate(main.transform);
        }

        public override void SlashInBehaviour()
        {
            slashView.transform.localRotation = Quaternion.identity;
        }
        public override void SetDefaultTransform()
        {
            base.SetDefaultTransform();

            main.localPosition = _initPosition;

            mainLeft.localPosition = DefaultPos;
            mainLeft.localRotation = _mainLeftRotation;
            mainLeft.localScale = _mainLeftScale;

            mainRight.localPosition = DefaultPos;
            mainRight.localRotation = _mainRightRotation;
            mainRight.localScale = _mainRightScale;
        }

        public override void SlashUpdateBehaviour()
        {
            mover.MoveToDirection(Vector2.left * 2, mainLeft);
            mover.MoveToDirection(Vector2.right * 2, mainRight);

            rotator.FullRotateToDirection(mainLeft, -1);
            rotator.FullRotateToDirection(mainRight, 1);
        }
    }
}
