namespace winterStage
{
    public class HeartBlock : BoostBlock
    {
        public override void BonusEffect()
        {
            BonusHandler.OnHeartSlash.Invoke(transform.position);
        }
    }
}
