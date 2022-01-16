namespace InvoiceEngine.Core.Constants
{
    public static class Constants
    {
        public const int SpeedOfLight = 300000; // km per sec.

        public const string DefaultUploadFilePath = "~/UploadFiles/";

        public static readonly string[] AllowedFileTypes = { ".csv", ".xml" };

        public const string CSVDateFormat = "dd/MM/yyyy hh:mm:ss";
    }
}
