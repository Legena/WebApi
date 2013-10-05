using Spring.IO;
using Spring.Social.Dropbox.Api;
using Spring.Social.Dropbox.Connect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Helpers
{
    public class Dropbox
    {
        private const string DropboxAppKey = "5izqincotwfu3jv";
        private const string DropboxAppSecret = "xgj3csf09f1kr4f";
        private const string DropboxTokenValue = "d7ccyiqvo33byems";
        private const string DropboxTokenSecret = "bgdwet2c4q8em0f";

        public static string Upload(string name, string file)
        {
            DropboxServiceProvider dropboxServiceProvider =
                new DropboxServiceProvider(DropboxAppKey, DropboxAppSecret, AccessLevel.AppFolder);

            // Login in Dropbox
            IDropbox dropbox = dropboxServiceProvider.GetApi(DropboxTokenValue, DropboxTokenSecret);

            // Upload a file
            Entry uploadFileEntry = dropbox.UploadFileAsync(
                new FileResource(file),
                "/" + name + ".jpg").Result;

            // Share a file
            var sharedUrl = dropbox.GetMediaLinkAsync(uploadFileEntry.Path).Result;

            return sharedUrl.Url;
        }
    }
}