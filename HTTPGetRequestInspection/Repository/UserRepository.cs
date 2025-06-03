using HTTPGetRequestInspection.Models;

namespace HTTPGetRequestInspection.Repository
{
    public class UserRepository
    {
        public List<User> Users = new List<User>()
        {
            new User() { Id = 1, Name= "Pranaya", Email = "Pranaya@Example.com" },
            new User() { Id = 2, Name= "Anurag", Email = "Anurag@Example.com" },
            new User() { Id = 3, Name= "Priyanka", Email = " Priyanka@Example.com" }
        };

        public List<Order> Orders = new List<Order>()
        {
            new Order() { Id = 1001, UserId = 1, TotalAmount = 100 },
            new Order() { Id = 1002, UserId = 2, TotalAmount = 200 },
            new Order() { Id = 1003, UserId = 1, TotalAmount = 300 },
            new Order() { Id = 1004, UserId = 2, TotalAmount = 400 },
            new Order() { Id = 1005, UserId = 3, TotalAmount = 500 },
        };

        // Get all user
        public IEnumerable<User> GetAll()
        {
            var users = Users.ToList();

            foreach(var user in users)
            {
                user.Orders = Orders.Where(o => o.UserId == user.Id).ToList();
            }

            return users;
        }

        // Get user by ID
        public User GetById(int Id)
        {
            var user = Users.FirstOrDefault(u => u.Id == Id);

            if(user != null)
            {
                user.Orders = Orders.Where(o => o.UserId == user.Id).ToList();
            }

            return user!;

        }

        // Search users by name
        public IEnumerable<User> SearchByName(string name)
        {
            var users = Users.Where(u => u.Name.Contains(name)).ToList();

            foreach(var user in users)
            {
                user.Orders = Orders.Where(o => o.UserId == user.Id).ToList();
            }

            return users;
        }

        // Get orders by user ID
        public IEnumerable<Order> GetOrderByUserId(int UserId)
        {
            var userWithOrders = Orders.Where(o => o.UserId == UserId);

            return userWithOrders ?? new List<Order>();

        }
    }
}
