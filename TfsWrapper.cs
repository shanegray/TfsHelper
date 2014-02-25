using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TfsHelper {
    public class TfsWrapper {
        private readonly TfsTeamProjectCollection projectCollection;
        private readonly List<WorkspaceInfo> projectWorkspaces;

        public TfsWrapper(TfsOptions options, WorkspaceInfo[] workspaces) {
            this.projectCollection = new TfsTeamProjectCollection(new Uri(options.ProjectCollectionUrl));
            this.projectWorkspaces = workspaces.Where(w => w.Name.StartsWith(options.ProjectCollectionName, StringComparison.OrdinalIgnoreCase)).ToList();

            if (this.projectWorkspaces.Any() == false) throw new Exception("Unable to match to any project workspaces");

            // set the dev workspace
            this.DevWorkspace = this.SetSingleWorkspace("dev");
            this.PersonalWorkspace = this.SetPersonalWorkspace();            
        }       

        public Workspace DevWorkspace { get; private set; }
        public Workspace PersonalWorkspace { get; private set; }
        public Workspace ServiceWorkspace { get; private set; }

        public void SyncDownFromDev(bool force) {
            if (this.PersonalWorkspace.GetPendingChanges().Any() && force == false) {
                if (force == false) throw new Exception("There are pending changes waiting on your personal branch. Run a check-in or undo command");               
            }                        
        }

        public void SyncUpFromPersonal() {

        }

        #region private methods
        private Workspace SetPersonalWorkspace() {
            // set the personal workspaces
            var aliases = this.projectWorkspaces.Select(x => x.OwnerDisplayName.Split(' ')[0]).Distinct();

            foreach (var alias in aliases) {
                var workspace = this.SetSingleWorkspace(alias, false);
                if (workspace != null) return workspace;
            }

            throw new Exception("Unable to match to personal workspace");
        }

        private Workspace SetSingleWorkspace(string suffix, bool throwEx = true) {
            var single = this.projectWorkspaces.SingleOrDefault(w => w.Name.EndsWith(suffix, StringComparison.OrdinalIgnoreCase));
            if (single == null) {
                if (throwEx)
                    throw new Exception("Unable to match workspace with key: " + suffix);
                return null;
            }

            return single.GetWorkspace(this.projectCollection);
        }
        #endregion
    }
}
