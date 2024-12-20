using Documents;

namespace Clients
{
    interface IClient
    {
        MyList<Project> GetProjects();
        void AddToDataBase(string databaseName);
    }
}
