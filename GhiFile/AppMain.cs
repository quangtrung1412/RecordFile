using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace RecordFile;

public class AppMain : IApp
{
    private const char SeparateChar = (char)1;
    private readonly IFileService _fileService;
    private readonly string _folderPath;
    private readonly string _fileOutput;

    public AppMain(IFileService fileService, IConfiguration config)
    {
        _fileService = fileService;
        _folderPath = config["InputFolder"];
        _fileOutput = config["OutputFilePath"];

    }

    private DateTime? GetTag52ValueFromLine(string line)
    {
        string tag52KeyValue = Regex.Match(line, SeparateChar + "?52=\\d{8}\\s[\\d\\.:]+" + SeparateChar).Value;
        if (!string.IsNullOrEmpty(tag52KeyValue))
        {
            int tag52KeyValueLength = tag52KeyValue.Length;
            string tag52StringValue = tag52KeyValue.Substring(4, tag52KeyValueLength - 5);
            return DateTime.ParseExact(tag52StringValue, "yyyyMMdd hh:mm:ss.fff", CultureInfo.InvariantCulture);
        }
        return null;
    }

    private int LineComparisonByTag52(string line1, string line2)
    {
        DateTime? tag52KeyValue1 = GetTag52ValueFromLine(line1);
        DateTime? tag52KeyValue2 = GetTag52ValueFromLine(line2);
        if (tag52KeyValue1 != null && tag52KeyValue2 != null)
        {
            return tag52KeyValue1.Value.CompareTo(tag52KeyValue2.Value);
        }
        return 0;
    }
    private string LineReplaceByTag34(string line ,int stt){
            string tag34KeyValue =Regex.Match(line,SeparateChar+"?34=\\d+"+SeparateChar).Value;
            string tag34SubValue = tag34KeyValue.Substring(1,3);
            string tag34ReplaceValue =SeparateChar+tag34SubValue+$"{stt+1}"+SeparateChar;
            // replace
            return line.Replace(tag34KeyValue,tag34ReplaceValue);
    }
    public void Run()
    {
        var allLines = _fileService.GetAllLineFromTextFileInFolder(_folderPath);
        allLines.Sort(LineComparisonByTag52);
        List<string> newAllLine = new List<string>();
        for (int i =0 ; i<allLines.Count();i++)
        {
            string newLine =LineReplaceByTag34(allLines[i],i);
            newAllLine.Add(newLine);
        }
        _fileService.WriteToNewFile(_fileOutput,newAllLine);
    }

}