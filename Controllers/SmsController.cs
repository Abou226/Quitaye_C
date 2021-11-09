using Amazon.SimpleNotificationService.Model;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SmsController : GenericController<User, Sms>
    {
        private readonly IConfigSettings _settings;
        private readonly IGenericRepositoryWrapper<User, Sms> repositoryWrapper;
        public SmsController(IConfigSettings settings, IGenericRepositoryWrapper<User, Sms> _repositoryWrapper) : base(_repositoryWrapper)
        {
            _settings = settings;
            repositoryWrapper = _repositoryWrapper;
        }

        [HttpPost("otp_send")]
        public async Task<ActionResult<PublishResponse>> SendAsync([FromBody] Sms value)
        {
            if (value != null)
                return NotFound();

            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemA.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    Random rdn = new Random();
                    var code = rdn.Next(100000, 999999);
                    
                    value.Message = $"{code} est votre code de verification";
                    
                    var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(_settings.AccessKey, _settings.SecretKey);
                    var client = new Amazon.SimpleNotificationService.AmazonSimpleNotificationSer‌​viceClient(
                                                  awsCreden‌​tials, Amazon.RegionEndpoint.EUWest2);

                    var smsAttributes = new Dictionary<string, MessageAttributeValue>();

                    CancellationTokenSource source = new CancellationTokenSource();
                    CancellationToken token = source.Token;

                    PublishRequest publishRequest = new PublishRequest();
                    publishRequest.Message = value.Message;
                    publishRequest.MessageAttributes = smsAttributes;
                    publishRequest.PhoneNumber = value.Telephone;
                    publishRequest.Subject = value.SenderId;

                    var result = await client.PublishAsync(publishRequest, token);
                    if (result != null)
                    {
                        value.AuthorId = identity.First().Id;
                        value.Id = Guid.NewGuid();
                        value.SendDate = DateTime.Now;
                        var sms = await repositoryWrapper.ItemB.AddAsync(value);
                        await repositoryWrapper.SaveAsync();
                        return Ok(value);
                    }
                    else return null;
                }
                else return null;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("otp_check/{code}")]
        public async Task<ActionResult<PublishResponse>> CheckAsync([FromBody] JsonPatchDocument value, [FromRoute] string code)
        {
            if (value != null && !string.IsNullOrWhiteSpace(code))
                return NotFound();

            try
            {
                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await repositoryWrapper.ItemA.GetBy(x => x.Id.ToString().
                Equals(claim));
                if (identity.Count() != 0)
                {
                    var sms = await repositoryWrapper.ItemB.GetBy(x => x.Message.StartsWith(code) && x.AuthorId == identity.First().Id);
                    if(sms.Count() != 0)
                    {
                        var single = identity.First();
                        value.ApplyTo(single);
                        await repositoryWrapper.SaveAsync();
                    }
                    return Ok(value);
                }
                else return null;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
