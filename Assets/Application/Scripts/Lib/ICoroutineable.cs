using System.Collections;

namespace winterStage
{
    public interface ICoroutineable
    {
        public IEnumerator Routine { get; set; }

        public void Start(IEnumerator routine);

        public void Stop(IEnumerator routine);
    }
}
