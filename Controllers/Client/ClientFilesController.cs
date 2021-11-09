﻿using AutoMapper;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientFilesController : GenericController<File, Client>
    {
        private readonly IConfigSettings _settings;
        private readonly IFileManager _fileManager;
        private readonly IGenericRepositoryWrapper<File, Client> wrapper;
        public ClientFilesController(IConfigSettings settings,
            IFileManager fileManager,
            IGenericRepositoryWrapper<File, Client> _wrapper):base(_wrapper)
        {
            _settings = settings;
            _fileManager = fileManager;
            wrapper = _wrapper;
        }

        [HttpPost("upload")]
        public async Task<ActionResult> Upload([FromForm] File value)
        {
            try
            {
                if (value == null)
                    return NotFound();

                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await wrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    
                    if (value.Image != null)
                    {
                        var result = await _fileManager.Upload(_settings.AccessKey, _settings.SecretKey, _settings.BucketName, Amazon.RegionEndpoint.USEast1, value.Image);
                        value.Url = result.Url;
                    }
                    return Ok(value);
                }
                else return NotFound("User not identified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost("download")]
        public async Task<ActionResult> Download([FromBody] File value)
        {
            try
            {
                if (value == null)
                    return NotFound();

                var claim = (((ClaimsIdentity)User.Identity).Claims.FirstOrDefault(x => x.Type == "Id").Value);
                var identity = await wrapper.ItemB.GetBy(x => x.Id.ToString().
                Equals(claim));

                if (identity.Count() != 0)
                {
                    var name = value.Url.Split('/');
                    var filename = "";
                    foreach (var item in name)
                    {
                        filename = item;
                    }
                    await _fileManager.Download(_settings.AccessKey, _settings.SecretKey, _settings.BucketName, Amazon.RegionEndpoint.USEast1, filename);
                    return Ok(value);
                }
                else return NotFound("User not identified");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}