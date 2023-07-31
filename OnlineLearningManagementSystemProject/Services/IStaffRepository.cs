using OnlineLearningManagementSystemProject.Models;
using OnlineLearningManagementSystemProject.ViewModel;

namespace OnlineLearningManagementSystemProject.Services
{
    public interface IStaffRepository
    {
        List<Staff> GetStaff();
        Staff GetStaffById(string userId);
        Staff GetStaffById(int Id);
        int CreateStaff(IFormFile formFile, Staff stff, string userId);
        int deleteStaff(int staffid);
        int updateStaff(int id, Staff sff);
        List<NationalIDStaffNameViewModel> GetNationalIDStaffNames();
    }
}
