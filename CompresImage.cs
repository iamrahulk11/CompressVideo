
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Enums;

namespace practice
{
    //--------Tried With Xabe.FFmpeg
    public class CompresImage
    {
        public static async Task Main()
        {
            string inputFilePath = "C:\\Users\\Rahul\\Videos\\Captures\\ClipTry.mp4";
            var mediaInfo = await MediaInfo.Get(inputFilePath);

            Console.WriteLine(mediaInfo.Size);

            bool check = await ConvertVideoAsync();
            if (check)
            {
                Console.WriteLine("Conversion completed successfully.");
            }
            else
            {
                Console.WriteLine("Conversion failed.");
            }
            Console.ReadLine();
        }

        public static async Task<bool> ConvertVideoAsync()
        {
            try
            {
                string inputFilePath = "C:\\Users\\Rahul\\Videos\\Captures\\ClipTry.mp4";
                string outputFilePath = "F:\\RAHUL\\Converted.mp4";

                if (File.Exists(inputFilePath))
                {
                    FFmpeg.ExecutablesPath = @"C:\Users\Rahul\ffmpeg\bin\ffmpeg.exe";//Path.Combine("F:\\RAHUL\\practice\\practice\\FFmpeg", "ffmpeg.exe");

                    var mediaInfo = await MediaInfo.Get(inputFilePath);
                   
                    var videoStream = mediaInfo.VideoStreams.First();
                    var targetFileSizeInBytes = 8 * 1024 * 1024; // 8MB in bytes
                    
                    long videoDurationInSeconds = Convert.ToInt64(mediaInfo.Duration.TotalSeconds); // Total Duration of Video

                    // Calculate the target bitrate in bits per second (BPS)
                    int targetBitrateInBps = (int)(targetFileSizeInBytes * 8 / videoDurationInSeconds);

                    videoStream
                        .SetCodec(VideoCodec.Libx264)
                        //.SetSize(VideoSize.Hd480)
                        .SetBitrate(targetBitrateInBps);

                    await Conversion.New()
                        .AddStream(videoStream)
                        .SetOutput(outputFilePath)
                        .Start();

                    return true;
                }
                else
                {
                    Console.WriteLine("Input file does not exist.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return false;
            }
        }
    }
}

//-----------END----------------


//---------------Tried With FFmpeg **Taking Long Time becs exe file is need to be searched**

        //public static void Main()
        //{
        //    string result = string.Empty;
        //    string directoryPath = "F:\\RAHUL";
        //    string fileName = "ClipTry.mp4"; // Replace with the actual file name

        //    // Combine the directory path and file name to get the full file path
        //    string filePath = Path.Combine(directoryPath, fileName);

        //    if (File.Exists(filePath))
        //    {
        //        // Read the file into a byte array
        //        byte[] fileBytes = File.ReadAllBytes(filePath);

        //        using (MemoryStream ms = new MemoryStream(fileBytes))
        //        {
        //            CustomHttpPostedFile postedFile = new CustomHttpPostedFile(
        //                fileName,
        //                "file/content-type",
        //                ms
        //            );

        //            if (postedFile != null && postedFile.ContentLength > 0)
        //            {
        //                // Specify the input and output file paths
        //                string inputFilePath = "F:\\RAHUL\\" + postedFile.FileName;
        //                string outputFilePath = "F:\\RAHUL\\NewVid\\" + postedFile.FileName;

                      
        //                //  postedFile.SaveAs(inputFilePath);
        //                //double targetFileSizeMB = 10.0;

        //                //// Calculate the target bitrate based on the file size
        //                //int targetBitrateKbps = (int)(targetFileSizeMB * 8192);



        //                // Use FFmpeg to compress the video
        //                string ffmpegPath = (@"C:\Users\Rahul\ffmpeg\bin\ffmpeg.exe"); // Path to your FFmpeg executable
        //                    //-vf "scale=1280:720" -c:v libx264 -crf 23 -preset medium -c:a aac -b:a 120k
        //                string arguments = $"-i \"{inputFilePath}\" -vf scale=1280:720 -c:v libx264 -crf 23 -preset medium  -b:v 8000k \"{outputFilePath}\"";
        //               // string arguments = $"-i \"{inputFilePath}\" -vf scale=1280x720 -c:v h264_amf \"{outputFilePath}\"";
        //                ProcessStartInfo psi = new ProcessStartInfo(ffmpegPath)
        //                {
        //                    UseShellExecute = false,
        //                    CreateNoWindow = true,
        //                    RedirectStandardOutput = true,
        //                    RedirectStandardError = true,
        //                    Arguments = arguments
        //                };
        //                try
        //                {
        //                    // Process proc;
        //                    // proc = new Process();
        //                    // proc.StartInfo.FileName = psi.FileName;
        //                    // proc.StartInfo.Arguments = psi.Arguments;
        //                    // proc.StartInfo.UseShellExecute = false;
        //                    // proc.StartInfo.CreateNoWindow = false;
        //                    // proc.StartInfo.RedirectStandardOutput = true;
        //                    // proc.StartInfo.RedirectStandardError = true;
        //                    // proc.Start();
        //                    // proc.WaitForExit();
        //                    //string StdOutVideo = proc.StandardOutput.ReadToEnd();
        //                    using (Process process = new Process { StartInfo = psi })
        //                    {
        //                        process.Start();
        //                        process.WaitForExit();
        //                        //process.Close();
        //                    }

        //                }
        //                catch (Exception ex)
        //                {
        //                    result = ex.ToString();
        //                }
        //                Console.WriteLine(result);      
        //            }
        //        }
        //    }
        //    Console.WriteLine("ok");
        //    Console.ReadLine();
        //}
//    }
//    public class CustomHttpPostedFile : HttpPostedFileBase
//    {
//        private readonly string _fileName;
//        private readonly string _contentType;
//        private readonly Stream _stream;

//        public CustomHttpPostedFile(string fileName, string contentType, Stream stream)
//        {
//            _fileName = fileName;
//            _contentType = contentType;
//            _stream = stream;
//        }

//        public override int ContentLength => (int)_stream.Length;

//        public override string ContentType => _contentType;

//        public override string FileName => _fileName;

//        public override Stream InputStream => _stream;

//        public override void SaveAs(string filename)
//        {
//            using (var fileStream = File.Create(filename))
//            {
//                _stream.CopyTo(fileStream);
//            }
//        }
//    }
//}

//------------------------END---------------
