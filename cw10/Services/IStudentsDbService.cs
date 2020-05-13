using cw10.DTOs.Request;
using cw10.DTOs.Response;


namespace cw10.Services
{
    public interface IStudentsDbService
    {
        public GetStudentsResponse GetStudents();
        public void ModifyStudent(ModifyStudentRequest request);
        public void DeleteStudent(DeleteStudentRequest request);
        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request);
        public PromoteStudentsResponse PromoteStudents(PromoteStudentRequest request);

    }
}
