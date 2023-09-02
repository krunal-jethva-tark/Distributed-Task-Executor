using Newtonsoft.Json;

namespace DTS.Models.Tasks.TaskType.MemeDownloader
{
    public class MemeDownloaderTask : ITaskType
    {
        public async Task<DTSTask> ExecuteTask(DTSTask task)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://meme-api.com/gimme/wholesomememes");
            if (response.IsSuccessStatusCode && response.Content != null)
            {
                try
                {
                    var responseResult = JsonConvert.DeserializeObject<MemeDownloadModel>(response?.Content?.ToString() ?? "{}");
                    if (responseResult?.Nsfw ?? true)
                        task.Status = Utility.Enum.TaskStatus.Failed;
                    else
                    {
                        DownloadAndUploadImage(responseResult);
                        task.Status = Utility.Enum.TaskStatus.Completed;
                    }
                }
                catch (Exception ex)
                {
                    task.ErrorMessage = ex.Message;
                    task.Status = Utility.Enum.TaskStatus.Failed;
                }
            } else
            {
                task.Status = Utility.Enum.TaskStatus.Failed;
            }


            return task;
        }

        private static async void DownloadAndUploadImage(MemeDownloadModel responseResult)
        {
            using var httpClient = new HttpClient();
            string imageFileName = responseResult.Url[(responseResult.Url.LastIndexOf('/') + 1)..];
            using (HttpResponseMessage imageResponse = await httpClient.GetAsync(responseResult.Url))
            using (var imageStream = await imageResponse.Content.ReadAsStreamAsync())
            using (var fileStream = File.Create(imageFileName))
            {
                imageStream.CopyTo(fileStream);
            }
            Console.WriteLine($"Image saved as {imageFileName}");
        }
    }
}
