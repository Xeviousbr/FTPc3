using System;
using System.Collections.Generic;
using System.Net.Http;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Script.Serialization;
using WS_PROCERGS;

namespace Ws_Lac
{
    public partial class Ws_Lac : ServiceBase
    {
        private Timer timer;
        private Boolean PrimeiraVez = true;
        public Ws_Lac()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this.timer = new Timer(600000D);  // 600000 milliseconds = 600 seconds 10 min
            //this.timer = new Timer(28800000D);  // 28800000 milliseconds = 8 horas
            this.timer.AutoReset = true;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Elapsed);
            this.timer.Start();
        }

        protected override void OnStop()
        {
            this.timer.Stop();
            this.timer = null;
        }
        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {

                Logs.WriteLog("INICIO");
                Task t = Task.Run(() => ExecutaWs.Executa(this.PrimeiraVez));
                t.Wait();
                Logs.WriteLog("FIM");
            }
            catch(Exception exc)
            {
                Logs.WriteLog("Error thread:" + exc.Message);
            }

        }

    }
}
