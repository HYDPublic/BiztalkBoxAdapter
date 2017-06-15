using Box.V2;
using Box.V2.Auth;
using Box.V2.Config;
using Box.V2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBoxSDK_VS2015
{
    class Program
    {

        private const string CLIENT_ID= "tikm6z227unvdy7538xepph50kh6e6xv";
        private const string CLIENT_SECRET = "WH1WVLgKFDZCZsEL4CRYLSULiAPbCgIh";

        private static void Main(string[] args)
        {
            try
            {
                new Program().ExecuteMainAsync().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.ReadKey();
            }
        }


        private async Task ExecuteMainAsync()
        {
            
            var devToken = "E3myQeNXgY2PA5q2AQaSqLbEUIVabPeU";
            var fileName = "test.txt";
            var localFilePath = @"C:\Users\Public\test.txt";
            var parentFolderId = "1";

            var timer = Stopwatch.StartNew();

            

            var config = new BoxConfig(CLIENT_ID, CLIENT_SECRET, new Uri("http://boxsdk"));
                                        //DevToken
            var auth = new OAuthSession(devToken, "NOT_NEEDED", 3600, "bearer");
            var client = new BoxClient(config, auth);

            var file = File.OpenRead(localFilePath);
            var fileRequest = new BoxFileRequest
            {
                Name = fileName,
                Parent = new BoxFolderRequest { Id = parentFolderId }
            };

            var bFile = await client.FilesManager.UploadAsync(fileRequest, file);

            Console.WriteLine("{0} uploaded to folder: {1} as file: {2}", localFilePath, parentFolderId, bFile.Id);
            Console.WriteLine("Time spend : {0} ms", timer.ElapsedMilliseconds);
            Console.ReadKey();
        }
    }
}
