namespace TfsHelper {
    public class TfsOptions {        

        public TfsOptions(string[] args) {
            var argsParser = new ArgumentParser(args);
            this.ProjectCollectionName = argsParser.GetValue("-project", "tfs-testproject");
            this.ProjectCollectionUrl = argsParser.GetValue("-url", "https://shanegray.visualstudio.com/DefaultCollection/");
        }

        public string ProjectCollectionName { get; private set; }
        public string ProjectCollectionUrl { get; private set; }
    }
}
