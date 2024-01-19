

using BenkienApp.Data.Entity;
using BenkienApp.Service.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _service.GetAllUserByIncludeAsync();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<User> Get(int id)
        {
            return await _service.GetUserByIncludeAsync(id);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] User value)
        {
			await _service.AddAsync(value);
            await _service.SaveAsync();
            return Ok();
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] User value)
        {
            var mainUser = await _service.GetUserByIncludeAsync(id);

            if (mainUser != null)
            {
                //mainUser.Id = id;
                //mainUser.RoleId = id;
                mainUser.Name = value.Name;
                mainUser.Surname = value.Surname;
                mainUser.UserName = value.UserName;
                mainUser.Email = value.Email;
                mainUser.Phone = value.Phone;
                mainUser.Image = value.Image;
                mainUser.AdressDetail = value.AdressDetail;
                mainUser.Password = value.Password;
                mainUser.IsAdmin = value.IsAdmin;
                mainUser.IsActive = value.IsActive;
                mainUser.CreatedAt = value.CreatedAt;

                mainUser.CreateDate = DateTime.UtcNow;
                if (value.RoleId is not null)
                {
                mainUser.RoleId = value.RoleId;
                }
				_service.Update(mainUser);
                var response = await _service.SaveAsync();
                if (response > 0)
                {
                    return Ok();
                }
            }
            return Problem();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            User mainUser = await _service.FindAsync(id);

            if (mainUser is not null)
            {
                _service.Delete(mainUser);
                var response = await _service.SaveAsync();

                if (response > 0)
                {
                    return Ok();

                }
            }
            return BadRequest();
        }
    }
}
