﻿using System.IO;

namespace Zaimoni.Data
{
    public class OutTextFile
    {
        public readonly string filepath;
        private StreamWriter _file;

        public OutTextFile(string dest) {
            filepath = dest;
        }

        public void WriteLine(string src) {
            lock (this) {
                if (null == _file) {
                    if (File.Exists(filepath)) File.Delete(filepath);
                    _file = File.CreateText(filepath);
                }
                _file.WriteLine(src);
                _file.Flush();
            }
        }

        public void Close() {   // XXX C# goes inefficient with a true destructor so don't provide one
            lock (this) {
                if (null != _file) {
                    _file.Close();
                    _file = null;
                }
            }
        }
    }
}
