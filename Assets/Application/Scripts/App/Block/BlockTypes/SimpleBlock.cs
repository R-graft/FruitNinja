using UnityEngine;

namespace winterStage
{
    public class SimpleBlock : Block
    {
        public Transform mainLeft;
        public Transform mainRight;

        public Transform shadowLeft;
        public Transform shadowRight;

        public override void SetDefaultTransform()
        {
            base.SetDefaultTransform();

            mainLeft.localPosition = DefaultPos;
            mainLeft.localRotation = DefaultRot;
            mainRight.localRotation = DefaultRot;
            mainRight.localPosition = DefaultPos;

            shadowRight.localPosition = DefaultPos;
            shadowRight.localRotation = DefaultRot;
            shadowLeft.localPosition = DefaultPos;
            shadowLeft.localRotation = DefaultRot;
        }

        public override void SlashUpdateBehaviour()
        {
            mover.MoveToDirection(Vector2.left * 2, mainLeft);
            mover.MoveToDirection(Vector2.right * 2, mainRight);

            mover.MoveToDirection(Vector2.left * 2, shadowLeft);
            mover.MoveToDirection(Vector2.right * 2, shadowRight);

            rotator.RotateToDirection(mainLeft, -1);
            rotator.RotateToDirection(mainRight, 1);
            rotator.RotateToDirection(shadowRight, 1);
            rotator.RotateToDirection(shadowLeft, -1);
        }
    }
}
