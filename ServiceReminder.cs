using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService
{
    public partial class ServiceReminder : ServiceBase
    {

        public static String PATH_FILE = "../../FileUsers.csv";
        public static String PATH_TEMP_FILE = "../../Temp.csv";
        public static char SEPARATOR = ';';

        public ServiceReminder()
        {
            InitializeComponent();
            eventLog = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("ServiceMail"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "ServiceMail", "Application");
            }
            eventLog.Source = "ServiceMail";
            eventLog.Log = "Application";
        }

        protected override void OnStart(string[] args)
        {
            eventLog.WriteEntry("El servicio de envio de email se inicio");
            this.readFile();
        }

        protected override void OnStop()
        {
            eventLog.WriteEntry("El servicio de envio de email se ha detenido");
        }

        public void readFile()
        {
            StreamReader fileRead = new StreamReader(PATH_FILE);
            StreamWriter fileWriter = new StreamWriter(PATH_TEMP_FILE);
            string line;
            fileWriter.WriteLine(line = fileRead.ReadLine());
            while ((line = fileRead.ReadLine()) != null)
            {
                string[] person = line.Split(SEPARATOR);
                string email = person[3];
                DateTime executeDate = DateTime.Parse(person[4]);
                Boolean result = sendEmail(executeDate, email);
                if (!result)
                {
                    fileWriter.WriteLine(line);
                }
            }
            fileRead.Close();
            fileWriter.Close();
            File.Delete(PATH_FILE);
            File.Move(PATH_TEMP_FILE, PATH_FILE);
        }

        public Boolean sendEmail(DateTime dateIn, string email)
        {
            Boolean result = false;
            if (DateTime.Compare(DateTime.Today, dateIn) == 0)
            {
                EmailManager emailManager = new EmailManager();
                String message = emailManager.sendEmail("noreplaynoreplay176@gmail.com", "tnwibhtmmagzddgi", email, "Prueba envio", "Esto es una prueba de envio");
                eventLog.WriteEntry(message);
                result = true;
            }
           return result; 
        }
    }
}
