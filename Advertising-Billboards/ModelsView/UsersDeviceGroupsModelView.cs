using Advertising_Billboards.Models;
using System.Collections.Generic;

namespace Advertising_Billboards.ModelsView
{
    public class UsersDeviceGroupsModelView
    {
        public IEnumerable<User> Users { get; set; }

        public IEnumerable<DeviceGroup> DeviceGroups { get; set; }

        public UsersDeviceGroupsModelView(IEnumerable<User> user, IEnumerable<DeviceGroup> deviceGroups)
        {
            Users = user;
            DeviceGroups = deviceGroups;
        }
    }
}
