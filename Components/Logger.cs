using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Components
{
    public class Logger
    {
        public string DirectoryPath { get; private set; }
        public string FileName { get; private set; }
        public int Level { get; set; } /* 1 = ERROR, 2 = WARN, 3 = INFO, 4 = DEBUG, 5 = DEBUG MEDIUM, 6 = DEBUG HIGH */
        StreamWriter writter;

        public Logger(string dirPath, string fName, int level) {

            this.DirectoryPath = dirPath;
            this.Level = level;
            this.FileName = fName;

            if (!this.TestDirPath()) {
                throw new Exception($"Given directory {this.DirectoryPath} path does not exist");
            }
            if (!this.TestLevel()) {
                throw new Exception($"Given level '{this.Level}' is out of range 1-7 ...");
            }
            if (!this.TestFileName())
            {
                throw new Exception($"Filename is not valid: {this.FileName}");
            }

            this.CreateLogFile();
        }

        public Logger(string dirPath, string fName, int level, bool forceCreateDir)
        {

            this.DirectoryPath = dirPath;
            this.Level = level;

            if (!this.TestDirPath() && forceCreateDir)
            {
                Directory.CreateDirectory(dirPath);
            }
            else {
                throw new Exception($"Given directory {this.DirectoryPath} path does not exist");
            }
            if (!this.TestLevel())
            {
                throw new Exception($"Given level '{this.Level}' is out of range 1-7 ...");
            }
            if (!this.TestFileName()){
                throw new Exception($"Filename is not valid: {this.FileName}");
            }

            this.CreateLogFile();
        }

        /* Public Methods */

        public void DebugHigh(string message) {
            if (this.Level <= 6)
            {
                writter.WriteLine(this.TimesTamp() + " DEBUG-H: " + message);
            }
        }

        public void DebugMedium(string message) {
            if (this.Level <= 5)
            {
                writter.WriteLine(this.TimesTamp() + " DEBUG-M: " + message);
            }
        }

        public void Debug(string message) {

            if (this.Level <= 4) {
                writter.WriteLine(this.TimesTamp() + " DEBUG: "  + message);
            }
        }

        public void Info(string message) {
            if (this.Level <= 3)
            {
                writter.WriteLine(this.TimesTamp() + " INFO: " + message);
            }
        }

        public void Warn(string message) {
            if (this.Level <= 2)
            {
                writter.WriteLine(this.TimesTamp() + " WARN: " + message);
            }
        }

        public void Error(string message) {
            if (this.Level <= 1)
            {
                writter.WriteLine(this.TimesTamp() + " ERROR: " + message);
            }
        }

        /* Private Methods */
        private string TimesTamp() {

            return DateTime.UtcNow.ToUniversalTime().ToString();
        }

        private void CreateLogFile() {

            writter = new StreamWriter(this.DirectoryPath + "\\" + this.FileName + ".txt");
        }

        private bool TestFileName() {

            return true;
        }

        private bool TestLevel() {

            return (this.Level > 0 && this.Level < 8);
        }

        private bool TestDirPath() {

            return Directory.Exists(this.DirectoryPath);
        }
    }
}
