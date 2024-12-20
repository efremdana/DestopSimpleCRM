
namespace Documents
{
    public class Project : IProject   
    {
        public string Name { get; }

        public statusProject Status { get; set; }
        private Ingener ingener = new Ingener();

        public Project()
        {
            Name = string.Empty;
        }

        public Project(string name)
        {
            Name = name;
            Status = statusProject.isDoing;
        }

        public void TickStatus()
        {
            if (Status != statusProject.Finish && ingener.GetAnswer())
                Status = statusProject.Finish;
        }
        public bool Equals(Project p)
        {
            return Name.Equals(p.Name);
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
