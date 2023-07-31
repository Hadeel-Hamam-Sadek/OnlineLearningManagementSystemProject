using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineLearningManagementSystemProject.Models;

namespace OnlineLearningManagementSystemProject.Services
{
    public class UploadStaffFileRepository : IUploadStaffFileRepository
    {
        LMSContext context;

        public UploadStaffFileRepository(LMSContext _context)
        {
            context = _context;
        }

        public int DeleteMaterial(int materialId)
        {
            var material = context.Materials.FirstOrDefault(m => m.MaterialID == materialId);
            if (material != null)
            {
                context.Materials.Remove(material);
                int raw = context.SaveChanges();
                return raw;
            }
            return -1;
        }

        public IEnumerable<Material> GetAll()
        {
            return context.Materials;
        }

        public IEnumerable<Material> GetLecturesByMaterial(int corseid)
        {
            return context.Materials
                .Include(m => m.Staff)
                .Include(m => m.Course)
                .Where(m => m.CourseID == corseid);
        }

        public List<Material> GetLecturesByMaterialDoctor(int corseid, string UserId)
        {
            return context.Materials
               .Include(m => m.Staff)
               .Include(m => m.Course)
               .Where(m => m.Staff.UserID == UserId && m.CourseID == corseid).ToList();
        }


        public Course GetLecturesByMaterialCourse(int corseid)
        {
            return context.Courses

               .Where(m => m.CourseId == corseid).FirstOrDefault();
        }

        public int UploadDocument(IFormFile formFile, [Bind("MaterialName,CourseID,StaffId")] Material material, string userId)
        {
            if (material.formFile != null && material.formFile.Length > 0)
            {
                string FileName = Guid.NewGuid().ToString() + Path.GetExtension(material.formFile.FileName);
                string RelativePath = Path.Combine("Documents", FileName);
                string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", RelativePath);
                using (var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    material.formFile.CopyTo(fileStream);
                }
                material.MaterialType = MaterialType.Document;
                material.MaterialPath = RelativePath; // Store the relative path instead of the full physical path
                material.MaterialDate = DateTime.Today.ToString();
                material.StaffId = context.Staffs.FirstOrDefault(s => s.UserID == userId).StaffId;
                context.Add(material);
                int raw = context.SaveChanges();
                return raw;
            }
            return -1;

        }
        public int UploadLink([Bind("MaterialPath")] string materialPath, Material material)
        {
            if (Uri.IsWellFormedUriString(materialPath, UriKind.Absolute))
            {
                material.MaterialType = MaterialType.Link;
                material.MaterialDate = DateTime.Today.ToString();
                material.MaterialPath = materialPath;

                context.Add(material);
                int raw = context.SaveChanges();
                return raw;
            }

            return -1;
        }

        public int UploadVideo(IFormFile formFile, [Bind("MaterialName,CourseID,StaffId")] Material material)
        {
            if (formFile != null && formFile.Length > 0)
            {
                if (IsVideoFile(formFile.FileName))
                {
                    string FileName = Guid.NewGuid().ToString() + Path.GetExtension(material.formFile.FileName);
                    string RelativePath = Path.Combine("Videos", FileName);
                    string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", RelativePath);
                    using (var fileStream = new FileStream(FilePath, FileMode.Create))
                    {
                        formFile.CopyTo(fileStream);
                    }
                    material.MaterialType = MaterialType.Video;
                    material.MaterialPath = RelativePath; // Store the relative path instead of the full physical path
                    material.MaterialDate = DateTime.Today.ToString();
                    context.Add(material);
                    int raw = context.SaveChanges();
                    return raw;
                }
            }
            return -1;
        }


        private bool IsVideoFile(string fileName)
        {
            string[] videoExtensions = { ".mp4", ".avi", ".mov", ".mkv" };
            string extension = Path.GetExtension(fileName);
            return videoExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase);
        }
    }
}
