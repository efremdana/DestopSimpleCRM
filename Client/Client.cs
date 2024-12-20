using Documents;
using System.IO;

namespace Clients
{
    public class Client : LID, IClient
    {
        private MyList<Project> projects;
        private TextWriter loggingFileWriter;



        public Client(string name, int number, string NameProject, Sell sell) : base(name, number)
        {
            Name = name; Number = number;
            Project p = new Project(NameProject);
            projects = new MyList<Project> { p };
            Sell = sell;
            Sell.Project = p; 
        }

        public static statusProject GetStatusProject(Client client, Project project)
        {
            client.projects[project].TickStatus();
            return client.projects[project].Status;
        }

        public override void Dispose()
        {
            base.Dispose();
            if (loggingFileWriter != null) 
            { 
                loggingFileWriter.WriteLine();
                loggingFileWriter.Flush();
                loggingFileWriter.Close();
            }
        }
        private bool CheckStr(string line)
        {
            return line.Equals($"{Number} Client:");
        }

        public void AddToDataBase(string databaseName)
        {
            string str = string.Format("Client №{0}:\nName: {1}\nProjects: {2}", Number, Name, projects);
            if (loggingFileWriter == null)
            {
                if (!File.Exists(databaseName))
                {
                    loggingFileWriter = File.CreateText(databaseName);
                    loggingFileWriter.WriteLine("Client base");
                    loggingFileWriter.WriteLine(str);
                }
                else
                {
                    if (InDataBase(databaseName))
                    {
                        OverWriting(databaseName);
                    }
                    else
                    {
                        loggingFileWriter = File.AppendText(databaseName);
                        loggingFileWriter.WriteLine(str);
                    }
                }
            }
            else
            {
                OverWriting(databaseName);
            }
        }
        private void OverWriting(string databaseName)
        {
            string[] lines = File.ReadAllLines(databaseName);
            for (int i = 0; i < lines.Length; i++)
            {
                if (CheckStr(lines[i]))
                {
                    lines[i + 2] = $"Projects: {projects}";
                    break;
                }
            }
            File.WriteAllLines(databaseName, lines);
        }
        private bool InDataBase(string databaseName)
        {
            string[] lines = File.ReadAllLines(databaseName);
            foreach (string line in lines)
            {
                if (CheckStr(line)) return true;
            }
            return false;
        }

        public MyList<Project> GetProjects()
        {
            return projects;
        }
    }
}
