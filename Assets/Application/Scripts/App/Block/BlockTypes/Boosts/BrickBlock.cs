namespace winterStage
{
    public class BrickBlock : BoostBlock
    {
        public override void BonusEffect()
        {
            BonusController.OnBrickSlash();
        }
    }
}
