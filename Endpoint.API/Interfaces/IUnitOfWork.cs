namespace Endpoint.API.Interfaces
{
    public interface IUnitOfWork
    {
        public ITasksRepository TasksRepository { get; }
        public void Commit();
    }
}
