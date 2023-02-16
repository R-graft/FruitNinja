namespace winterStage
{
    public class BasketBlock : BoostBlock
    {
        public override void BonusEffect()
        {
            BonusController.OnBasketSlash.Invoke(transform.position);
        }
    }
}
