using UnityEngine;

namespace winterStage
{
    public class BoostBlock : Block, IBonusBlock
    {
        public override void SlashInBehaviour()
        {
            BonusEffect();

            partsRenderers[0].enabled = false;
            partsRenderers[1].enabled = false;

            slashView.flyingTrail.enabled = false;
        }
        public override void SetDefaultTransform()
        {
            base.SetDefaultTransform();

            partsRenderers[0].enabled = true;
            partsRenderers[1].enabled = true;

            slashView.flyingTrail.enabled = true;
        }

        public virtual void BonusEffect()
        {
        }
    }
}
