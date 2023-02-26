namespace winterStage
{
    public class MagneteBlock : BoostBlock
    {
        public override void BonusEffect()
        {
            BonusHandler.OnMagnetSlash(transform);
        }
    }
}
