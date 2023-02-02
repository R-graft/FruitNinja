using UnityEngine;

namespace winterStage
{
    public class DisableState : State
    {
        public Transform _transform;
        public DisableState(Transform transform)
        {
            _transform = transform;
        }

        public override void Enter()
        {
            _transform.localScale = Vector3.one;

            _transform.localRotation = Quaternion.identity;

            _transform.position = Vector3.zero;
        } 
    }
}
