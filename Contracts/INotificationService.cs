using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface INotificationService
    {
        Task<Notification> SendTo(string userId, Notification value);
        Task<Notification> SendToTopic(string topicName, Notification value);
    }
}
