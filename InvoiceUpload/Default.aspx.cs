using InvoiceEngine.Core.Constants;
using InvoiceEngine.Services.Services;
using System;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace InvoiceUpload
{
    public partial class _Default : Page
    {
        FileUploadService fileUploadService = new FileUploadService();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Upload(object sender, EventArgs e)
        {
            try
            {
                //Upload and save the file.
                string filename = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string extension = Path.GetExtension(filename);
                string filePath = Server.MapPath(Constants.DefaultUploadFilePath) + filename; ;
                FileUpload1.SaveAs(filePath);

                bool isValidFileType = Constants.AllowedFileTypes.Contains(extension);

                if (!isValidFileType)
                {
                    //  file is InvalidInvalid  
                    throw new Exception("Unknown Format");
                }

                //if file is valid
                if (extension == ".csv")
                {
                    fileUploadService.UploadCSV(filePath);
                }
                else if (extension == ".xml")
                {
                    fileUploadService.UploadXML(filePath);
                }

                Response.StatusCode = 200;
                Response.Write("Successfully Uploaded");

            }
            catch (FormatException ex)
            {
                Response.StatusCode = 400;
                Response.Write($"{ex.Message} is not in the correct format.");                
            }

            catch (Exception ex)
            {
                Response.StatusCode = 400;
                Response.Write(ex.Message);
              
            }
            finally
            {

            }
        }
    }
}