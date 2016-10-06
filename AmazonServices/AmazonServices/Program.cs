using System;

using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.S3.Model;
using System.IO;

namespace AmazonServices
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
                Console.WriteLine(" The file with the name " + fileNameInS3 + " was succesfully sent to cloud");
            }
            catch(AmazonS3Exception S3Ex)
            {
                Console.WriteLine(S3Ex.Message, S3Ex.InnerException);
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
            catch(AmazonS3Exception S3Ex)
            {
                Console.WriteLine(S3Ex.Message, S3Ex.InnerException);
            }

        }

        public void CreateBucket(string nameOfBucket)
        {
            ListBucketsResponse bucketList = _client.ListBuckets();
            bool found = false;

            for(int i = 0; i < bucketList.Buckets.Count && !found; ++i)
            {
                if(bucketList.Buckets[i].BucketName == nameOfBucket)
                {
                    found = true;
                }
            }

            if(!found)
            {
                try
                {
                    _client.PutBucket(new PutBucketRequest().BucketName = nameOfBucket);
                    Console.WriteLine(" The bucket with the name " + nameOfBucket + "was succesfully created");
                }
                catch(AmazonS3Exception S3Ex)
                {
                    Console.WriteLine(S3Ex.Message, S3Ex.InnerException);
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
                Console.WriteLine("The object with the name " + fileNameInS3 + " was succesfully deleted");
            }
            catch(AmazonS3Exception s3Ex)
            {
                Console.WriteLine(s3Ex.Message, s3Ex.InnerException);
            }
        }

        public void DeleteBucket(string bucketName)
        {
            try
            {
                _client.DeleteBucket(new DeleteBucketRequest { BucketName = bucketName });
                Console.WriteLine("The bucket with the name " + bucketName + " was succesfully deleted");
            }
            catch(AmazonS3Exception S3Ex)
            {
                Console.WriteLine(S3Ex.Message, S3Ex.InnerException);
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
                        Console.WriteLine(" The file was downloaded sucessfully ");
                    }
                }
            }
            catch(AmazonS3Exception S3Ex)
            {
                Console.WriteLine(S3Ex.Message, S3Ex.InnerException);
            }
        }

        public ListObjectsResponse GetListOfObjectForASpecificBucket(string bucketName)
        {
            
            ListObjectsRequest listRequest = new ListObjectsRequest
            {
                BucketName = bucketName
            };

            ListObjectsResponse listObjects = _client.ListObjects(listRequest);
            return listObjects;
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
            
            foreach(dynamic entry in listObjects.S3Objects)
            {
                request.AddKey(entry.Key);
            }
            request.BucketName = bucketName;

            try
            {
                _client.DeleteObjects(request);
                Console.WriteLine(" The objects from the folder " + folderName + " was succesfully deleted ");
            }
            catch(AmazonS3Exception S3Ex)
            {
                Console.WriteLine(S3Ex.Message, S3Ex.InnerException);
            }
        }

        
    }
    class Program
    {
        static void Main(string[] args)
        {

            AmazonUploader uploader = new AmazonUploader();

            //Upload a file to cloud
            //string fileFromDisk = @"C:\Users\Administrator\Downloads\fisier.txt"; //local file name
            //string bucketName = "vasilut2"; // name of the bucket
            //string directoryNameInBucket = ""; // subDirectory in bucket
            //string fileNameInS3 = "vasilut fisier.txt"; //file name up in S3
            //uploader.SendFile(fileFromDisk, bucketName, directoryNameInBucket, fileNameInS3);
            //uploader.SendFileAnotherMethod(fileFromDisk, bucketName, directoryNameInBucket, fileNameInS3);

            //Create a bucket
            //uploader.CreateBucket("vasilut2");

            //Delete a file from a bucket
            //uploader.DeleteFile("vasilut2", "vasilut fisier.txt");

            //Delete a bucket
            //uploader.DeleteBucket("vasilut2");

            //download files
            //uploader.DownloadFile("testlucian", "lucian3 fisier.txt");

            //get specific objects for a bucket
            //uploader.GetListOfObjectForASpecificBucket("testlucian");

            //get specific object for a folder
            //uploader.GetListOfObjectForASpecificFolder("vasilut2", "folder1");

            //delete a folder
            //uploader.DeleteAFolder("vasilut2", "folder1");

            Console.Write("Press any key to continue..");
            Console.ReadLine();
        }
    }
}
