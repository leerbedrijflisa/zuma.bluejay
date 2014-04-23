using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lisa.Zuma.BlueJay.WebApi.Helpers
{
    public class StorageHelper 
    {
        public StorageHelper(string connectionName, string containerName)
        {
            this.connectionName = connectionName;
            this.containerName = containerName;
        }

        public string GetFileNameFromSasUri(string url)
        {
            var uri = new Uri(url);

            return GetFileNameFromSasUri(uri);
        }

        public string GetFileNameFromSasUri(Uri url)
        {
            var fileName = url.Segments.Last();
            return fileName;            
        }

        public Uri GetWriteableSasUri(string fileName, TimeSpan expire)
        {
            return GetSASUri(SharedAccessBlobPermissions.Write, fileName, expire);
        }

        public Uri GetReadableSasUri(string fileName, TimeSpan expire)
        {
            return GetSASUri(SharedAccessBlobPermissions.Read, fileName, expire);
        }

        public bool RemoveFromStorageByName(string blobName)
        {
            var blockBlob = BlobContainer.GetBlockBlobReference(blobName);
            
            return blockBlob.DeleteIfExists();
        }

        public bool RemoveFromStorageByUri(string url)
        {
            var uri = new Uri(url);
            
            return RemoveFromStorageByUri(uri);
        }

        public bool RemoveFromStorageByUri(Uri url)
        {
            var blobName = url.Segments.Last();
            
            return RemoveFromStorageByName(blobName);
        }

        #region Public Properties
        public CloudBlobClient BlobClient
        {
            get
            {
                if (blobClient == null)
                {
                    blobClient = StorageAccount.CreateCloudBlobClient();
                }

                return blobClient;
            }
        }

        public CloudBlobContainer BlobContainer
        {
            get
            {
                if (blobContainer == null)
                {
                    blobContainer = BlobClient.GetContainerReference(containerName);
                    blobContainer.CreateIfNotExists();
                }

                return blobContainer;
            }
        }

        public CloudStorageAccount StorageAccount
        {
            get
            {
                if (storageAccount == null)
                {
                    var connectionString = CloudConfigurationManager.GetSetting(connectionName);
                    storageAccount = CloudStorageAccount.Parse(connectionString);
                }

                return storageAccount;
            }
        }
        #endregion

        #region Private Helper Methods
        private Uri GetSASUri(SharedAccessBlobPermissions permissions, string fileName, TimeSpan expire)
        {
            var blockBlob = BlobContainer.GetBlockBlobReference(fileName);
            var policy = new SharedAccessBlobPolicy
            {
                Permissions = permissions,
                SharedAccessStartTime = DateTime.UtcNow,
                SharedAccessExpiryTime = DateTime.UtcNow.Add(expire)
            };

            var url = blockBlob.Uri.AbsoluteUri + blockBlob.GetSharedAccessSignature(policy);
            return new Uri(url);
        }
        #endregion

        #region Private Fields
        private string connectionName;
        private string containerName;

        private static CloudBlobClient blobClient;
        private static CloudBlobContainer blobContainer;
        private static CloudStorageAccount storageAccount;
        #endregion
    }
}