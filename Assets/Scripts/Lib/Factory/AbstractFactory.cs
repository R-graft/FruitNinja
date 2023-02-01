using UnityEngine;
namespace winterStage
{
    public abstract class AbstractFactory<T> : MonoBehaviour
    {
        public abstract T CreateObject();
    }
}
