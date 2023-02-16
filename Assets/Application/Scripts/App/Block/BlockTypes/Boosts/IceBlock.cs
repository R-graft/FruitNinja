namespace winterStage
{
    public class IceBlock : BoostBlock
    {
        public override void BonusEffect()
        {
            BonusController.OnIceSlash.Invoke();
        }
    }
}
