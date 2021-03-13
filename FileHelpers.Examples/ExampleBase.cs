using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace FileHelpers
{
    [TestFixture]
    public abstract class ExampleBase
    {
        /// <summary>
        ///     This property allows inheritors to call Console.Method() just like the static Console class.
        ///     This fake console captures the output. The output is used for the documentation generation.
        /// </summary>
        protected VirtualConsole Console => new VirtualConsole();

        /// <summary>Before each test, the input in the directory of the test is moved.</summary>
        [Test]
        public void ExecuteExample()
        {
            var binaryDirectory = GetCompileDirectory();
            var projectDirectory = GetProjectDirectory(binaryDirectory);
            var testDirectory = GetTestDirectory(projectDirectory);
            MoveFile(testDirectory, binaryDirectory);

            Run();
        }

        protected abstract void Run();

        private static void MoveFile(DirectoryInfo testDirectory, DirectoryInfo binaryDirectory)
        {
            const string InputFileName = "input.txt";
            var sourceFullName = Path.Combine(testDirectory.FullName, InputFileName);
            var targetFullName = Path.Combine(binaryDirectory.FullName, InputFileName);

            File.Copy(sourceFullName, targetFullName, true);
        }

        private DirectoryInfo GetTestDirectory(DirectoryInfo projectDirectory)
        {
            const string NamespaceOfProject = "FileHelpers";
            const string NamespaceDelimiter = ".";
            var fullNamespace = GetType().Namespace;
            Assert.NotNull(fullNamespace);
            var relativeNamespace = fullNamespace.Substring(NamespaceOfProject.Length + NamespaceDelimiter.Length);
            var relativeFoldersFromProject = relativeNamespace.Replace(NamespaceDelimiter, "/");

            var testDir = Path.Combine(projectDirectory.FullName, relativeFoldersFromProject);

            return new DirectoryInfo(testDir);
        }

        private static DirectoryInfo GetProjectDirectory(DirectoryInfo binaryDirectory)
        {
            const string WorkDirRelativeToProject = "bin/Debug/net40";
            var depth = WorkDirRelativeToProject.Split('/').Length;
            var projectDirectory = binaryDirectory;
            for (var i = 0; i < depth; i++)
            {
                var parent = projectDirectory.Parent;
                Assert.NotNull(parent);
                projectDirectory = parent;
            }

            return projectDirectory;
        }

        private DirectoryInfo GetCompileDirectory()
        {
            var toExecutableThing = GetType().Assembly.Location;
            var di = new FileInfo(toExecutableThing).Directory;
            return di;
        }
    }
}