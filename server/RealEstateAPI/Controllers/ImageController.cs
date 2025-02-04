using Imagekit.Models;
using Imagekit.Sdk;
using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RealEstateAPI.Controllers
{
	[ApiController]
	[Route("api/Image")]
	public class ImageController : ControllerBase
	{
		ImagekitClient imagekit = new ImagekitClient("public_0O6fWt547b835DVrknwauJAFZpQ=",
			"private_Ik1bzDk1h5m3KMW4v5bZw8pmQzk=", "https://ik.imagekit.io/aoy2r8vra7/");

		[HttpPost]
		[Route("upload")]
		public IActionResult Index(List<IFormFile> photos)
		{
			if (photos == null || photos.Count == 0)
			{
				return BadRequest("No files received.");
			}

			var filesInBytes = new List<byte[]>(); // List to store byte arrays
			var responses = new List<string>();
			foreach (var file in photos)
			{
				// Convert IFormFile to byte[]
				using (var memoryStream = new MemoryStream())
				{
					file.CopyTo(memoryStream); // Copy file stream to memory stream
					var fileBytes = memoryStream.ToArray(); // Convert to byte array
					filesInBytes.Add(fileBytes); // Add byte array to the list
					FileCreateRequest request = new FileCreateRequest
					{
						file = fileBytes,
						fileName = file.FileName
					};
					Result resp = imagekit.Upload(request);
					responses.Add(resp.url);
				}
			}
			
			return Ok(responses);
		}
		[HttpGet]
		[Route("getphoto")]
		public IActionResult GetPhotoById(string id)
		{
			Result res1 = imagekit.GetFileDetail(id);

			return Ok(res1);
		}
		[HttpGet]
		[Route("getphotobyurlAndDelete")]
		public IActionResult GetPhotoByUrl(string url)
		{
			try
			{

				var name = Path.GetFileNameWithoutExtension(url) + Path.GetExtension(url);
				GetFileListRequest model = new GetFileListRequest
				{
					Name = name
				};

				ResultList res = imagekit.GetFileListRequest(model);
				JArray jsonString = JArray.Parse(res.Raw);
				var firstElement = jsonString[0];
				string id = (string)firstElement["fileId"];
				ResultDelete res2 = imagekit.DeleteFile(id);

				return Ok(res2);
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					return StatusCode(500, ex.InnerException.Message);
				}
				return StatusCode(500, ex.Message);
			}
		}
	}
}
