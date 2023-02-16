namespace winterStage
{
    public class HeartBlock : BoostBlock
    {
        public override void BonusEffect()
        {
            BonusController.OnHeartSlash.Invoke(transform.position);
        }
    }
}
