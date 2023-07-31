using Microsoft.AspNetCore.Mvc;
using OnlineLearningManagementSystemProject.Models;

namespace OnlineLearningManagementSystemProject.Services
{
    public interface IUploadStaffFileRepository
    {
        int UploadDocument(IFormFile formFile, [Bind("MaterialName,CourseID,StaffId")] Material material, string userId);
        int UploadLink([Bind("MaterialPath")] string materialPath, Material material);
        int UploadVideo(IFormFile formFile, [Bind("MaterialName,CourseID,StaffId")] Material material);
        IEnumerable<Material> GetAll();
        IEnumerable<Material> GetLecturesByMaterial(int corseid);
        List<Material> GetLecturesByMaterialDoctor(int corseid, string UserId);
        int DeleteMaterial(int materialId);
        Course GetLecturesByMaterialCourse(int corseid);
    }
}
