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
        /// <summary>
        /// Creates a new instance of the StorageHelper.
        /// </summary>
        /// <param name="connectionName">The name of the connection as defined in the application config file.</param>
        /// <param name="containerName">The name of the container in which to store the data.</param>
        public StorageHelper(string connectionName, string containerName)
        {
            this.connectionName = connectionName;
            this.containerName = containerName;
        }

        /// <summary>
        /// Extracts the filename from the given sas uri.
        /// </summary>
        /// <param name="url">The sas uri from which the filename should be extracted.</param>
        /// <returns>The extracted filename.</returns>
        public string GetFileNameFromSasUri(string url)
        {
            var uri = new Uri(url);

            return GetFileNameFromSasUri(uri);
        }

        /// <summary>
        /// Extracts the filename from the given sas uri.
        /// </summary>
        /// <param name="url">The sas uri from which the filename should be extracted.</param>
        /// <returns>The extracted filename.</returns>
        public string GetFileNameFromSasUri(Uri url)
        {
            var fileName = url.Segments.Last();
            return fileName;            
        }

        /// <summary>
        /// Gets a writable sas uri. This uri can be written until <paramref name="expire"/>.
        /// </summary>
        /// <param name="fileName">The filename to create the uri for.</param>
        /// <param name="expire">The time until the uri invalidates.</param>
        /// <returns>The writable sas uri.</returns>
        public Uri GetWriteableSasUri(string fileName, TimeSpan expire)
        {
            return GetSASUri(SharedAccessBlobPermissions.Write, fileName, expire);
        }

        /// <summary>
        /// Gets a writable sas uri. This uri can be written until <paramref name="expire"/>.
        /// </summary>
        /// <param name="fileName">The filename to create the uri for.</param>
        /// <param name="expire">The time until the uri invalidates.</param>
        /// <returns>The writable sas uri.</returns>
        public Uri GetReadableSasUri(string fileName, TimeSpan expire)
        {
            return GetSASUri(SharedAccessBlobPermissions.Read, fileName, expire);
        }

        /// <summary>
        /// Removes the item from storage identified by <paramref name="blobName"/>.
        /// </summary>
        /// <param name="blobName">The name of the file to delete from storage.</param>
        /// <returns>True if success, false otherwise.</returns>
        public bool RemoveFromStorageByName(string blobName)
        {
            var blockBlob = BlobContainer.GetBlockBlobReference(blobName);
            
            return blockBlob.DeleteIfExists();
        }

        /// <summary>
        /// Removes the item from storage identified by <paramref name="url"/>.
        /// </summary>
        /// <param name="url">The url of the item to delete.</param>
        /// <returns>True if success, false otherwise.</returns>
        public bool RemoveFromStorageByUri(string url)
        {
            var uri = new Uri(url);
            
            return RemoveFromStorageByUri(uri);
        }

        /// <summary>
        /// Removes the item from storage, specified by <paramref name="url"/>.
        /// </summary>
        /// <param name="url">The url of the item to delete.</param>
        /// <returns>True if success, false otherwise.</returns>
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