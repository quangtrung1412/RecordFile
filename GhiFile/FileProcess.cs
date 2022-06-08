namespace RecordFile;
public delegate void FileEventHandler(object sender,FileEventArgs e);
public class FileEventArgs :EventArgs{
    public int PercentComplete{get;set;}=0;
    //public string Message {get;set;}
}