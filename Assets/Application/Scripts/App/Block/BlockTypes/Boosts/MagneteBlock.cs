namespace winterStage
{
    public class MagneteBlock : BoostBlock
    {
        public override void BonusEffect()
        {
            BonusController.OnMagnetSlash(transform);
        }
    }
}
