namespace winterStage
{
    public class BrickBlock : BoostBlock
    {
        public override void BonusEffect()
        {
            BonusHandler.OnBrickSlash();
        }
        public override void SlashInBehaviour()
        {
             BonusEffect();
        }
    }
}
