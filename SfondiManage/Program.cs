using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SfondiManage
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(false, "Global\\" + "9c3a4956-3d6c-42bf-acf5-ab03834fe63b"))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("Il programma è già in esecuzione", "PROGRAMMA ATTIVO", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    return;
                }
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                }
                catch (Exception ef)
                {
                    MessageBox.Show(ef.Message,"Problema",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }


        }
    }
}
