namespace winterStage
{
    public class IceBlock : BoostBlock
    {
        public override void BonusEffect()
        {
            BonusHandler.OnIceSlash.Invoke();
        }
    }
}
