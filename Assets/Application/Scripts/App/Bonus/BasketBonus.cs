using System.Collections.Generic;
using UnityEngine;

namespace winterStage
{
    public class BasketBonus : Bonus
    {
        private BlocksController _blocks;

        private SpawnSystem _spawner;

        private Vector2 _firstDirection;
        private Vector2 _secondDirection;

        public BasketBonus(BlocksController blocks, SpawnSystem spawner, Vector2 firtsDir, Vector2 secondDir)
        {
            _blocks = blocks;
            _spawner = spawner;
            _firstDirection= firtsDir;
            _secondDirection= secondDir;
        }

        public void BasketBonusAction(Vector3 basketPos)
        {
            var newBasket = new Queue<Block>();

            newBasket = _spawner.GetCurrentPack(newBasket, true);

            while (newBasket.Count > 0)
            {
                var block = newBasket.Dequeue();

                block.transform.position = new Vector3(basketPos.x, basketPos.y + 2, block.transform.position.z);

                float directionX = Random.Range(_firstDirection.x, _secondDirection.x);
                float directionY = Random.Range(_firstDirection.y, _secondDirection.y);

                Vector2 newDirection = new Vector2(directionX, directionY);

                block.StateMashine.SetState(new ActiveState(block, newDirection, 0, Vector3.one));

                _blocks.AddBlock(block, true);
            }
        }
    }
}
