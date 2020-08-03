using Advertising_Billboards.Models;
using System.Collections.Generic;

namespace Advertising_Billboards.ModelsView
{
    public class UsersDevicesModelView
    {
        public IEnumerable<User> Users { get; set; }

        public IEnumerable<Device> Devices { get; set; }

        public UsersDevicesModelView(IEnumerable<User> users, IEnumerable<Device> devices)
        {
            Users = users;
            Devices = devices;
        }

    }
}
