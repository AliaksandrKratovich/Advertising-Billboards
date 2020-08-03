using Advertising_Billboards.Models;
using DataBaseAccess.Repsitory;
using ModelServices.AdvertisingServicing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Alturos.VideoInfo;
using Microsoft.AspNetCore.Hosting;

namespace ModelServices
{
    public class AdvertisingService : IAdvertisingService
    {

        private readonly IRepository<Advertisement> _AdvRepository;
        private readonly IRepository<Device> _deviceRepository;
        private string _dir;
        public AdvertisingService(IRepository<Device> deviceRepository, IRepository<Advertisement> repository, IHostingEnvironment env)
        {
            _AdvRepository = repository;
            _deviceRepository = deviceRepository;
            _dir = env.WebRootPath;
        }

        public IEnumerable<Advertisement> GetAdvertisements(long deviceId)
        {
            if (deviceId == 0)
            {
                return _AdvRepository.GetAll().ToList();
            }
            return _AdvRepository.GetAll().Where(a => a.Device.Id == deviceId).ToList();
        }

        public void DeleteAdvertising(long advId)
        {
            Advertisement advertisement = _AdvRepository.Get(advId);
            DeleteFile(advertisement.Path);
            advertisement.IsDeleted = true;
            _AdvRepository.Delete(advertisement);
        }

        private void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public void Save(Advertisement advert)
        {
            _AdvRepository.Update(advert);
        }
        public string AddAdvertising(Advertisement advertisement, long deviceId, long memoryLength)
        {
            Device device = _deviceRepository.Get(deviceId);

            advertisement.Device = _deviceRepository.Get(deviceId);
            advertisement.MaxTime = (long)GetVideoDuration().Result;

            var lastAdvId = _AdvRepository.GetAll().LastOrDefault()?.Id ?? 0;

            advertisement.FileName = $"d-{deviceId}-adv-{lastAdvId + 1}.mp4";

            var directoryVideosPath = CheckCreatingOfRepository();
            var filePath = Path.Combine(directoryVideosPath, advertisement.FileName);

            advertisement.Path = filePath;
            advertisement.MemoryLength = memoryLength;

            device.Advertisements?.Add(advertisement);
            _AdvRepository.Create(advertisement);
            _deviceRepository.Update(device);

            return filePath;
        }

        private async Task<double> GetVideoDuration()
        {
            var videoFilePath = Path.Combine(_dir, "file.mp4");
            var videoAnalyer = new VideoAnalyzer(Path.Combine(_dir, "ffmpeg","ffprobe.exe"));
            var analyzeResult = await videoAnalyer.GetVideoInfoAsync(videoFilePath);

            return analyzeResult.VideoInfo.Format.Duration;
        }

        private string CheckCreatingOfRepository()
        {
            var directoryVideosPath = Path.Combine(_dir, "Videos");
            if (!Directory.Exists(directoryVideosPath))
            {
                Directory.CreateDirectory(directoryVideosPath);
            }

            return directoryVideosPath;
        }
    }
}
