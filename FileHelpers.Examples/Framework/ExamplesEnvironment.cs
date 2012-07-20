using System.Collections.Generic;
using System.Collections;
using System;
using System.IO;
using System.Text;

namespace ExamplesFramework
{
    public static class ExamplesEnvironment
    {
        private static string mTemporaryFolder;
        private static string mPreviousCurrentDirectory;

        public static string TemporaryFolder
        {
            get
            {
                if (string.IsNullOrEmpty(mTemporaryFolder))
                {
                    var guid = Guid.NewGuid().ToString();
                    mTemporaryFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Examples-" + guid);
                    //mTemporaryFolder = Path.Combine(Path.GetTempPath(), "Examples-" + guid);
                }
            
                return mTemporaryFolder;
            }
            set { mTemporaryFolder = value; }
        }

        private static void EnsureDirectoryExists(string tempPath)
        {
            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);
        }
       
        public static void Dispose()
        {
            DeleteAllFolder(TemporaryFolder);
        }

        private static void DeleteAllFolder(string folder)
        {
            if (mPreviousCurrentDirectory != null)
                Environment.CurrentDirectory = mPreviousCurrentDirectory;
            try
            {
                if (Directory.Exists(folder))
                    Directory.Delete(folder, true);
            }
            catch
            {}
        }

        private static readonly object mInitLock = new object();

        private static ExampleSandbox mLastSandbox;

        internal static void InitEnvironment(ExampleCode exampleCode)
        {
            lock (mInitLock)
            {
                if (mLastSandbox != null)
                {
                    mLastSandbox.StopWatching();
                    DeleteAllFolder(mLastSandbox.Path);
                }
                
                var sandbox = new ExampleSandbox(Path.Combine(TemporaryFolder, Path.GetFileNameWithoutExtension(exampleCode.OriginalFileName)));
                mLastSandbox = sandbox;

                EnsureDirectoryExists(sandbox.Path);
                if (Environment.CurrentDirectory != sandbox.Path)
                {
                    if (mPreviousCurrentDirectory == null)
                        mPreviousCurrentDirectory = Environment.CurrentDirectory;

                    Environment.CurrentDirectory = sandbox.Path;
                }

                foreach (var file in exampleCode.Files)
                {
                    if (file.Status == ExampleFile.FileType.InputFile)
                        File.WriteAllText(Path.Combine(sandbox.Path, file.Filename), file.Contents, Encoding.UTF8);
                }

                sandbox.Created += (sender, args) => exampleCode.OnNewFileCreated(args.FullPath);
                sandbox.StartWatching();

            }
        }

#region ExampleSandbox


		private sealed class ExampleSandbox
        {
            public ExampleSandbox(string path)
            {
                Path = path;
            }

            public string Path { get; set; }
            public event FileSystemEventHandler Created;

            private void OnCreated(FileSystemEventArgs e)
            {
                FileSystemEventHandler handler = Created;
                if (handler != null)
                    handler(this, e);
            }

            public void StartWatching()
            {
                if (Watcher == null)
                {
                    Watcher = new FileSystemWatcher(Path);
                    Watcher.NotifyFilter = NotifyFilters.FileName;
                    Watcher.Created += (sender, args) =>
                        {
                            OnCreated(args);
                        };
                    Watcher.EnableRaisingEvents = true;

                }
            }

            public void StopWatching()
            {
                if (Watcher != null)
                {
                    Watcher.EnableRaisingEvents = false;
                    Watcher.Dispose();
                    Watcher = null;
                }
            }

            private FileSystemWatcher Watcher { get; set; }
        }

    }
	#endregion   
}