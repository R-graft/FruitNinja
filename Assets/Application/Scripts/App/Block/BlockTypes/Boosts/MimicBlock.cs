using UnityEngine;

namespace winterStage
{
    public class MimicBlock : BoostBlock
    {
        public ParticleSystem particleEffect;
        public override void ActiveInBehaviour()
        {
            BonusHandler.OnMimicActive(this);
        }
    }
}