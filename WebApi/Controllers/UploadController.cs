using Spring.IO;
using Spring.Social.Dropbox.Api;
using Spring.Social.Dropbox.Connect;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class UploadController : ApiController
    {
        private const string DropboxAppKey = "5izqincotwfu3jv";
        private const string DropboxAppSecret = "xgj3csf09f1kr4f";
        private const string DropboxTokenValue = "d7ccyiqvo33byems";
        private const string DropboxTokenSecret = "bgdwet2c4q8em0f";

        public string Post()
        {
            DropboxServiceProvider dropboxServiceProvider =
                new DropboxServiceProvider(DropboxAppKey, DropboxAppSecret, AccessLevel.AppFolder);

            // Login in Dropbox
            IDropbox dropbox = dropboxServiceProvider.GetApi(DropboxTokenValue, DropboxTokenSecret);



            string root = Path.GetTempPath();

            var file = this.Request.Content.ReadAsByteArrayAsync().Result;

            File.WriteAllBytes(root + "\\upload.jpg", file);

            // Upload a file
            Entry uploadFileEntry = dropbox.UploadFileAsync(
                new FileResource(root + "\\upload.jpg"),
                "/" + Guid.NewGuid() + ".jpg").Result;

            // Share a file
            var sharedUrl = dropbox.GetMediaLinkAsync(uploadFileEntry.Path).Result;

            return sharedUrl.Url;
        }
    }
}
