namespace RecordFile;

public interface IFileService
{
    List<string> GetAllLineFromTextFile(string filePath);
    List<string> GetAllLineFromTextFileInFolder(string folderPath);
    List<string> GetAllFilePathFromFolder(string folderPath);
    void WriteToNewFile(string fileOutput,List<string> allNewLine);
}