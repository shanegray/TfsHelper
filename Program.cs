using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TfsHelper {
    class Program {
        static void Main(string[] args) {
            try {
                var wrapper = new TfsWrapper(new TfsOptions(args), Workstation.Current.GetAllLocalWorkspaceInfo());
                var argsParser = new ArgumentParser(args);

                Console.WriteLine("Working with personal workspace: {0}", wrapper.PersonalWorkspace.Name);
                Console.WriteLine("Working with dev workspace: {0}", wrapper.DevWorkspace.Name);

                wrapper.SyncDownFromDev(argsParser.HasKey("-force"));

                Console.WriteLine("Successfully synced from DEV: {1} to PERSON: - {0}", wrapper.PersonalWorkspace.Name, wrapper.DevWorkspace.Name);
            }
            catch (Exception ex) {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR : {0}", ex.Message);
                Console.ResetColor();
            }
        }
    }
}
