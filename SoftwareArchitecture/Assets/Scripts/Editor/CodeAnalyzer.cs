
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace WGADemo.Editor
{
    public static class CodeAnalyzer
    {
        private const string rootFolder = "Assets";
        private const string rootNamespace = "WGADemo";

        private class FolderStatistic
        {
            public int LinesCount { get; private set; }
            public int FilesCount { get; private set; }
            public void AddFile(int linesCount)
            {
                FilesCount++;
                LinesCount += linesCount;
            }
        }

        [MenuItem("WGADemo/Analyze Code")]
        public static void AnalyzeCode()
        {
            foreach (string filePath in GetAllCSharpFiles(rootFolder))
            {
                string[] lines = File.ReadAllLines(filePath);
                AnalyzeCSharpFile(filePath, ref lines);
                File.WriteAllLines(filePath, lines, System.Text.Encoding.UTF8);
            }
            Debug.Log("AnalyzeCode: done");
        }

        private static void AnalyzeCSharpFile(string filePath, ref string[] lines)
        {
            // remove any double empty lines
            for (int i = 1; i < lines.Length; i++)
            {
                bool prevLineEmpty = string.IsNullOrEmpty(lines[i - 1]);
                bool thisLineEmpty = string.IsNullOrEmpty(lines[i]);

                if (prevLineEmpty == true && thisLineEmpty == true)
                {
                    ArrayUtility.RemoveAt(ref lines, i);
                    i--;
                }
            }

            // be sure we have (one) empty line at file start
            if (string.IsNullOrEmpty(lines[0]) == false)
            {
                ArrayUtility.Insert(ref lines, 0, "");
            }

            // remove any empty lines at the end
            while (lines.Length > 0 && string.IsNullOrEmpty(lines[lines.Length - 1]) == true)
            {
                ArrayUtility.RemoveAt(ref lines, lines.Length - 1);
            }

            // remove trailing spaces
            char[] space = new char[] { ' ' };
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = lines[i].TrimEnd(space);
            }

            AnalyzeNamespace(filePath, ref lines);
        }

        private static void AnalyzeNamespace(string filePath, ref string[] lines)
        {
            string expectedNamespace = GetNamespaceFromPath(filePath);
            uint matchCounter = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line.StartsWith("namespace"))
                {
                    lines[i] = $"namespace {expectedNamespace}";
                    matchCounter++;
                    if (i == 0 || string.IsNullOrEmpty(lines[i - 1]) == false)
                    {
                        ArrayUtility.Insert(ref lines, i, "");
                        i++;
                    }
                }
            }
            if (matchCounter != 1)
            {
                string msg = $"namespace missing ({expectedNamespace})";
                PrintError("NamespaceMissing", msg, filePath, null);
            }
        }

        private static void PrintError(string type, string message, string pathToFile, uint? lineNumber)
        {
            Debug.LogError($"{pathToFile.Replace('\\', '/')}:{lineNumber} [{type}] -> {message}");
        }

        private static IEnumerable<string> GetAllCSharpFiles(string rootFolder)
        {
            foreach (string file in Directory.GetFiles(rootFolder))
            {
                if (file.ToLower().EndsWith(".cs"))
                {
                    yield return file;
                }
            }
            foreach (string folder in Directory.GetDirectories(rootFolder))
            {
                foreach (string file in GetAllCSharpFiles(folder))
                {
                    yield return file;
                }
            }
        }

        private static string GetNamespaceFromPath(string filePath)
        {
            string result = rootNamespace;
            int level = 2; // skip assets/scripts/
            while (true)
            {
                string nmPart = GetFolderFromPath(filePath, level);
                if (string.IsNullOrEmpty(nmPart) == true)
                {
                    return result;
                }
                else
                {
                    result += $".{nmPart}";
                    level++;
                }
            }
        }

        private static readonly char[] pathSeparators = new char[] { Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar };
        private static string GetFolderFromPath(string filePath, int folderLevel)
        {
            string[] path = Path.GetDirectoryName(filePath).Split(pathSeparators);
            if (path.Length > folderLevel)
            {
                return path[folderLevel];
            }
            return string.Empty;
        }
    }
}
