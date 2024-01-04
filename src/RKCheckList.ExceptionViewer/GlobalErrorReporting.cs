using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using RKCheckList.ExceptionViewer.Data;

namespace RKCheckList.ExceptionViewer;

public static class GlobalErrorReporting
{
    public static string GetErrorFileDirectoryAndEnsureCreated()
    {
        var errorDirectoryPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "RKCheckList");
        if (!Directory.Exists(errorDirectoryPath))
        {
            Directory.CreateDirectory(errorDirectoryPath);
        }
        return errorDirectoryPath;
    }

    public static string GenerateErrorFilePath(string errorDirectoryPath)
    {
        string errorFilePath;
        do
        {
            var errorGuid = Guid.NewGuid();
            errorFilePath = Path.Combine(
                errorDirectoryPath,
                $"Error-{errorGuid}.err");
        } while (File.Exists(errorFilePath));

        return errorFilePath;
    }

    public static void WriteExceptionInfoToFile(Exception exception, string targetFileName)
    {
        using var outStream = File.Create(targetFileName);
        
        var exceptionInfo = new ExceptionInfo(exception);
        JsonSerializer.Serialize(
            outStream, 
            exceptionInfo, 
            new JsonSerializerOptions(JsonSerializerDefaults.General)
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
    }
    
    /// <summary>
    /// Tries to find the absolute path to the viewer executable.
    /// </summary>
    public static bool TryFindViewerExecutable(out string executablePath)
    {
        executablePath = string.Empty;

        var executingAssembly = Assembly.GetExecutingAssembly();
        var executingAssemblyDirectory = Path.GetDirectoryName(executingAssembly.Location);
        var executingAssemblyName = executingAssembly.GetName().Name;
        if (string.IsNullOrEmpty(executingAssemblyDirectory) ||
            string.IsNullOrEmpty(executingAssemblyName))
        {
            return false;
        }
        
        var executablePathCheck = Path.Combine(executingAssemblyDirectory, executingAssemblyName);
        if (!Path.Exists(executablePathCheck))
        {
            executablePathCheck += ".exe";
            if (!Path.Exists(executablePathCheck))
            {
                return false;
            }
        }

        executablePath = executablePathCheck;
        return true;
    }

    /// <summary>
    /// Runs the viewer applications and waits for it to exit.
    /// </summary>
    public static void ShowGlobalException(string exceptionDetailsFilePath, string executablePath)
    {
        var processStartInfo = new ProcessStartInfo(
            executablePath,
            $"\"{exceptionDetailsFilePath}\"");
        processStartInfo.ErrorDialog = false;
        processStartInfo.UseShellExecute = false;

        var childProcess = Process.Start(processStartInfo);
        if (childProcess != null)
        {
            childProcess.WaitForExit();
        }
    }
}