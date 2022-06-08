namespace RecordFile;

public class FileService : IFileService
{
    //public FileEventHandler FileProgressEvent;
    public delegate void GetPercentProcess(string input);
    public  void WriteConsoleProcess(string input){
        Console.Write($"\rProcess....{input}");
        Thread.Sleep(2000);
    }
    public List<string> GetAllLineFromTextFile(string filePath)
    {
        return File.ReadAllLines(filePath).ToList();
    }

    public List<string> GetAllLineFromTextFileInFolder(string folderPath)
    {
        var listLines = new List<string>();
        var allFilePath = GetAllFilePathFromFolder(folderPath);
        int nuOfFile =allFilePath.Count();
        for(int i=0 ; i<nuOfFile;i++)
        {
            if(IsTxtFile(allFilePath[i]))
            {
                var allLineInFile = GetAllLineFromTextFile(allFilePath[i]);
                listLines.AddRange(allLineInFile);
            }
            //SendProcess(i+1,nuOfFile);
        }
        GetPercentProcess pc = WriteConsoleProcess;
        pc("50%");
        return listLines;
    }
    // private void SendProcess(int currentFileIndex,int nuOfFile){
    //     this.FileProgressEvent?.Invoke(this, new FileEventArgs{
    //         Message="",
    //         PercentComplete =(currentFileIndex*100)/(2*nuOfFile)
    //     });
    // }

    public List<string> GetAllFilePathFromFolder(string folderPath)
    {
        return Directory.GetFiles(folderPath).ToList();
    }

    private bool IsTxtFile(string? filePath)
    {
        if(!string.IsNullOrEmpty(filePath))
        {
            return Path.GetExtension(filePath).Equals(".txt");
        }
        return false;
    }
    public void WriteToNewFile(string fileOutput,List<string> allNewLine){
        var directoryOutput = Path.GetDirectoryName(fileOutput);
        if(directoryOutput!=null){
            if(Directory.Exists(directoryOutput)){
            if(File.Exists(fileOutput)){
             File.WriteAllLines(fileOutput,allNewLine);
            }
            
             File.WriteAllLines(fileOutput,allNewLine);
        }else{
            Directory.CreateDirectory(directoryOutput);
             File.WriteAllLines(fileOutput,allNewLine);
        }
        }
        GetPercentProcess pc = WriteConsoleProcess;
        pc("100%");
    }
    // public void WriteToFile(string filePath,string folderPath,string fileOutput){
    //     var Output= Path.Combine(folderPath,fileOutput);
    //     var dataFileInput = File.ReadAllLines(filePath);
        
    //      if(File.Exists(Output)){
    //          //var dataFileOuput = File.ReadAllLines(fileOutput);
    //          //dataFileOuput.Contains(dataFileInput)
            
    //         File.AppendAllLines(Output,dataFileInput);
    //     }
    //     else{
    //         File.WriteAllLines(Output,dataFileInput);
    //     }
    // }
}