using OnlineLearningManagementSystemProject.Models;

namespace OnlineLearningManagementSystemProject.Services
{
    public class DentityRepository : IdentityRepository
    {
        private readonly IHttpContextAccessor contextAccessor;
        LMSContext context;

        public DentityRepository(IHttpContextAccessor _contextAccessor, LMSContext _context)
        {
            contextAccessor = _contextAccessor;
            context = _context;
        }

        public string GetUserId(string userId)
        {
            return contextAccessor.HttpContext.Session.GetString("UserId");
        }
    }
}
