
namespace Documents
{
    public class Sell
    {
        public LIDContract Contract { get; set; }

        public Project Project { get; set; }

        public Sell(LIDContract contract) 
        {
            Contract = contract;
            Project = new Project();
        }
    }
}
