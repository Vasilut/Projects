

using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.IO;

namespace AmazonServicesApp.Services
{
    public class AmazonUploader
    {
        private IAmazonS3 _client;
        public AmazonUploader()
        {
            _client = AWSClientFactory.CreateAmazonS3Client(RegionEndpoint.USWest2);
        }

        public void SendFile(string localFilePath, string bucketName, string subDirectoryBucket, string fileNameInS3)
        {
            TransferUtility utility = new TransferUtility(_client);
            // making a TransferUtilityUploadRequest instance
            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();
            if (subDirectoryBucket == string.Empty || subDirectoryBucket == null)
            {
                request.BucketName = bucketName;
            }
            else
            {
                request.BucketName = bucketName + @"/" + subDirectoryBucket; // make a new folder in the bucket
            }
            request.Key = fileNameInS3; //file name up in S3
            request.FilePath = localFilePath; //local file name
            try
            {
                utility.Upload(request); //commensing the transfer
            }
            catch (AmazonS3Exception S3Ex)
            {
                //log exception
            }
        }

        public void SendFileAnotherMethod(string localFilePath, string bucketName, string subDirectoryBucket, string fileNameInS3)
        {
            PutObjectRequest request = new PutObjectRequest();

            if (subDirectoryBucket == string.Empty || subDirectoryBucket == null)
            {
                request.BucketName = bucketName;
            }
            else
            {
                request.BucketName = bucketName + @"/" + subDirectoryBucket; // make a new folder in the bucket
            }

            request.BucketName = bucketName;
            request.FilePath = localFilePath;
            request.Key = fileNameInS3;

            try
            {
                _client.PutObject(request);
                Console.WriteLine(" The file with the name " + fileNameInS3 + " was succesfully sent to cloud");
            }
            catch (AmazonS3Exception S3Ex)
            {
                //log exception
            }

        }

        public void CreateBucket(string nameOfBucket)
        {
            ListBucketsResponse bucketList = _client.ListBuckets();
            bool found = false;

            for (int i = 0; i < bucketList.Buckets.Count && !found; ++i)
            {
                if (bucketList.Buckets[i].BucketName == nameOfBucket)
                {
                    found = true;
                }
            }

            if (!found)
            {
                try
                {
                    _client.PutBucket(new PutBucketRequest().BucketName = nameOfBucket);
                }
                catch (AmazonS3Exception S3Ex)
                {
                    //log exception
                }
            }

        }

        public void DeleteFile(string bucketName, string fileNameInS3)
        {
            //delete a file from the first level of the bucket
            DeleteObjectRequest deleteObjectRequest = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = fileNameInS3
            };

            try
            {
                _client.DeleteObject(deleteObjectRequest);
            }
            catch (AmazonS3Exception s3Ex)
            {
                //log exception
            }
        }

        public void DeleteBucket(string bucketName)
        {
            try
            {
                _client.DeleteBucket(new DeleteBucketRequest { BucketName = bucketName });
            }
            catch (AmazonS3Exception S3Ex)
            {

            }
        }

        public void DownloadFile(string bucketName, string fileNameInS3)
        {
            GetObjectRequest objectRequest = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = fileNameInS3
            };

            try
            {
                using (GetObjectResponse responseDownload = _client.GetObject(objectRequest))
                {
                    string dest = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileNameInS3);
                    if (!File.Exists(dest))
                    {
                        responseDownload.WriteResponseStreamToFile(dest);
                    }
                }
            }
            catch (AmazonS3Exception S3Ex)
            {
                //log exception
            }
        }

        public List<FilesDto> GetListOfObjectForASpecificBucket(string bucketName)
        {

            ListObjectsRequest listRequest = new ListObjectsRequest
            {
                BucketName = bucketName
            };

            ListObjectsResponse listObjects = _client.ListObjects(listRequest);
            List<FilesDto> listFiles = new List<FilesDto>();

            foreach (dynamic entry in listObjects.S3Objects)
            {
                listFiles.Add(new FilesDto { FileName = entry.Key, Size = entry.Size });
            }

            return listFiles;
        }

        public ListObjectsResponse GetListOfObjectForASpecificFolder(string bucketName, string folderName)
        {
            ListObjectsRequest listRequest = new ListObjectsRequest
            {
                BucketName = bucketName,
                Prefix = folderName,
            };

            ListObjectsResponse listObjects = _client.ListObjects(listRequest);

            return listObjects;
        }

        public void DeleteAFolder(string bucketName, string folderName)
        {
            ListObjectsRequest listRequest = new ListObjectsRequest
            {
                BucketName = bucketName,
                Prefix = folderName,
            };

            DeleteObjectsRequest request = new DeleteObjectsRequest();
            ListObjectsResponse listObjects = _client.ListObjects(listRequest);

            foreach (dynamic entry in listObjects.S3Objects)
            {
                request.AddKey(entry.Key);
            }
            request.BucketName = bucketName;

            try
            {
                _client.DeleteObjects(request);
            }
            catch (AmazonS3Exception S3Ex)
            {
                //log exception
            }
        }
    }
}
