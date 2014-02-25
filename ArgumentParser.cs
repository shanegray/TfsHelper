using System;
using System.Collections.Generic;

namespace TfsHelper {
    public class ArgumentParser {
        private readonly List<string> args;
        public ArgumentParser(string[] args) { this.args = new List<string>(args); }

        public string GetValue(string argKey, string defaultValue = null) {
            var index = this.args.FindIndex(a => a.Equals(argKey, StringComparison.OrdinalIgnoreCase));
            if (index == -1) return defaultValue;

            return this.args[index + 1];
        }

        public bool HasKey(string argKey) {
            return this.args.FindIndex(a => a.Equals(argKey, StringComparison.OrdinalIgnoreCase)) != -1;
        }

    }
}
