using System;
using System.Collections.Generic;
using System.Text;
using Advertising_Billboards.Models;

namespace Server
{
    public interface IServerEmulation
    {
        Advertisement GetNextAdv(long deviceId);
        void AddAdvertisement(long deviceId, Advertisement advertisement);
        void DeleteAdvertisement(long deviceId, long advId);
        void ChangeFrequencyForDevice(long deviceId, int frequency);
        void ChangeFrequencyForDeviceGroup(long deviceGroupId, int frequency);
    }
}
