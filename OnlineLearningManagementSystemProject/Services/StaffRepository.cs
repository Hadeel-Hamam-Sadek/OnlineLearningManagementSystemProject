using OnlineLearningManagementSystemProject.Models;
using OnlineLearningManagementSystemProject.ViewModel;

namespace OnlineLearningManagementSystemProject.Services
{
    public class StaffRepository : IStaffRepository
    {
        LMSContext context;

        public StaffRepository(LMSContext _context)
        {
            context = _context;
        }

        public int CreateStaff(IFormFile formFile, Staff stff, string userId)
        {

            if (formFile != null && formFile.Length > 0)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(stff.formFile.FileName);
                string relativePath = Path.Combine("staffimage", fileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativePath);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }
                stff.StaffImage = relativePath;
                stff.UserID = userId;
                context.Staffs.Add(stff);
                int raw = context.SaveChanges();
                return raw;
            }
            else if (formFile == null || formFile.Length < 0)
            {
                string relativePath = "images/blank-profile-picture-973460_640-5-600x600.png";
                stff.StaffImage = relativePath;
                stff.UserID = userId;
                context.Staffs.Add(stff);
                int raw = context.SaveChanges();
                return raw;
            }
            return -1;
        }


        public int deleteStaff(int staffid)
        {
            Staff stf = context.Staffs.FirstOrDefault(s => s.StaffId == staffid);
            context.Staffs.Remove(stf);
            int raw = context.SaveChanges();
            return raw;
        }

        public List<NationalIDStaffNameViewModel> GetNationalIDStaffNames()
        {
            var staffs = context.Staffs.ToList();
            var NationalIDStaffNamelist = new List<NationalIDStaffNameViewModel>();
            foreach (var staff in staffs)
            {
                NationalIDStaffNamelist.Add(new NationalIDStaffNameViewModel
                {
                    staffid = staff.StaffId,
                    staffname = staff.FirstName + ' ' + staff.LastName,
                    nationalid = staff.NationalID,
                });
            }
            return NationalIDStaffNamelist;
        }

        public List<Staff> GetStaff()
        {
            return context.Staffs.ToList();
        }

        public Staff GetStaffById(string userId)
        {
            var staff = context.Staffs.FirstOrDefault(s => s.UserID == userId);
            return staff;
        }
        public Staff GetStaffById(int Id)
        {
            var staff = context.Staffs.FirstOrDefault(s => s.StaffId == Id);
            return staff;
        }

        public int updateStaff(int id, Staff sff)
        {
            Staff oldstf = context.Staffs.FirstOrDefault(s => s.StaffId == id);
            oldstf.FirstName = sff.FirstName;
            oldstf.LastName = sff.LastName;
            oldstf.NationalID = sff.NationalID;
            oldstf.Mobile = sff.Mobile;
            int raw = context.SaveChanges();
            return raw;
        }
    }
}
