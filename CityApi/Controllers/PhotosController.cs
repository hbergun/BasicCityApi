using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CityApi.Data;
using CityApi.Dtos;
using CityApi.Helpers;
using CityApi.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CityApi.Controllers
{
    [Route("api/cities/{cityid}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IAppRepository _appRepository;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private readonly Cloudinary _cloudinary;

        public PhotosController(IAppRepository appRepository,IMapper mapper,IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _appRepository = appRepository;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;
            Account account = new Account(_cloudinaryConfig.Value.CloudName,_cloudinaryConfig.Value.ApiKey,_cloudinaryConfig.Value.ApiSecret);
            _cloudinary = new Cloudinary(account);
        }

        [HttpPost]
        public IActionResult AddPhotoForCity(int cityid,[FromBody]PhotoForCreationDto photoForCreationDto)
        {
            City city = _appRepository.GetCityByID(cityid);
            if (city==null)
            {
                return BadRequest("Could Not Find City");
            }
            int currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (currentUserId != city.UserID)
            {
                return Unauthorized();
            }
            IFormFile file = photoForCreationDto.File;
            ImageUploadResult uploadResult = new ImageUploadResult();
            if (file.Length>0)
            {
                using (Stream stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File=new FileDescription(file.Name,stream)
                    };
                     uploadResult = _cloudinary.Upload(uploadParams);
                }
            }
            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicID = uploadResult.PublicId;
            Photo photo = _mapper.Map<Photo>(photoForCreationDto);
            photo.City = city;
            if (!city.Photos.Any(p=>p.IsMain))
            {
                photo.IsMain = true;
            }
            city.Photos.Add(photo);
            if (_appRepository.SaveAll())
            {
                PhotoForReturnDto photoForReturn = _mapper.Map<PhotoForReturnDto>(photo);
                return CreatedAtRoute("GetPhoto",new { id = photo.ID },photoForReturn);
            }
            return BadRequest("Could Not Add The Photo");
        }
        [HttpGet("{id}",Name ="GetPhoto")]
        public IActionResult GetPhoto(int id)
        {
            Photo photoFromDb = _appRepository.GetPhoto(id);
            var Photo = _mapper.Map<PhotoForReturnDto>(photoFromDb);
            return Ok(Photo);

        }
    }
}