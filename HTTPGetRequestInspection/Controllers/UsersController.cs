using HTTPGetRequestInspection.Models;
using HTTPGetRequestInspection.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HTTPGetRequestInspection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UsersController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //Endpoint to retrieve all users
        //GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            var users = _userRepository.GetAll();

            return Ok(users);
        }

        //Endpoint to fetch details of a specific user by Id
        //GET api/users/1
        [HttpGet]
        [Route("api/users/{Id}")]
        public ActionResult<User> GetUser([FromRoute] int Id)
        {
            var user = _userRepository.GetById(Id);

            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        //Endpoint for searching users by name (query parameter)
        //GET api/users/search?name=pranaya
        [HttpGet]
        [Route("search")]
        public ActionResult<IEnumerable<User>> SearchUsers([FromRoute] string name)
        {
                var users = _userRepository.SearchByName(name);

            if (users == null || !users.Any())
                return NotFound();

            return Ok(users);
        }

        //Endpoint to get all orders for a specific user
        //GET api/users/1/orders
        [HttpGet]
        [Route("api/users/{userId}/orders")]
        public ActionResult<IEnumerable<Order>> GetUserOrders([FromRoute] int userId)
        {
            var orders = _userRepository.GetOrderByUserId(userId);

            if(orders == null || !orders.Any())
                return NotFound();

            return Ok(orders);
        }
    }
}
