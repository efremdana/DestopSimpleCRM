
namespace Documents
{
    public enum statusProject
    {
        isDoing, Finish 
    }
    interface IProject
    {
        statusProject Status { get; }
        void TickStatus();

    }
}
