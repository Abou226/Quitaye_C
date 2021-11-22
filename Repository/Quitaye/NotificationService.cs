using Contracts;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Quitaye
{
    public class NotificationService : INotificationService
    {
        public async Task<Models.Notification> SendTo(string userId, Models.Notification value)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("private_key.json")
            });

            // This registration token comes from the client FCM SDKs.
            var registrationToken = userId;

            // See documentation on defining a message payload.
            var message = new Message()
            {
                Data = new Dictionary<string, string>()
                {
                    { "myData", "1337" },
                },
                Token = registrationToken,
                //Topic = "all",
                Notification = new FirebaseAdmin.Messaging.Notification()
                {
                    Title = value.Title,
                    Body = value.Message
                }
            };

            // Send a message to the device corresponding to the provided
            // registration token.
            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            // Response is a message ID string.
            if (!string.IsNullOrWhiteSpace(response))
            {
                return new Models.Notification() { Message = message.Notification.Body, Title = message.Notification.Title };
            }
            else return null;
        }

        public async Task<Models.Notification> SendToTopic(string topicName, Models.Notification value)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("private_key.json")
            });

            // This registration token comes from the client FCM SDKs.
            //var registrationToken = "TOKEN_HERE";

            // See documentation on defining a message payload.
            var message = new Message()
            {
                Data = new Dictionary<string, string>()
                {
                    { "myData", "1337" },
                },
                //Token = registrationToken,
                Topic = topicName,
                Notification = new FirebaseAdmin.Messaging.Notification()
                {
                    Title = value.Title,
                    Body = value.Message
                }
            };

            // Send a message to the device corresponding to the provided
            // registration token.
            string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
            // Response is a message ID string.
            if (!string.IsNullOrWhiteSpace(response))
            {
                return new Models.Notification() { Message = message.Notification.Body, Title = message.Notification.Title };
            }
            else return null;
        }
    }
}
