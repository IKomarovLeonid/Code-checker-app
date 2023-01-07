using Objects.Models;

namespace Processing.Workers
{
    public interface IWorker
    {
        public void Push(CodeSolution info);

        public void Start();

        public void Stop();
    }
}
