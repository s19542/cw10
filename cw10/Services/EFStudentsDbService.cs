using cw10.DTOs.Request;
using cw10.DTOs.Response;
using cw10.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;


namespace cw10.Services
{
    public class EfStudentsDbService : IStudentsDbService
    {

        private readonly s19542Context _dbContext;
        public EfStudentsDbService(s19542Context context)
        {
            _dbContext = context;
        }

        public void DeleteStudent(DeleteStudentRequest request)
        {
            var s = new Student
            {
                IndexNumber = request.IndexNumber
            };
            _dbContext.Attach(s);
            _dbContext.Remove(s);
            _dbContext.SaveChanges();

        }

        public EnrollStudentResponse EnrollStudent(EnrollStudentRequest request)
        {
            EnrollStudentResponse response = new EnrollStudentResponse();



            var studies = _dbContext.Studies
                                    .Where(s => s.Name.Equals(request.Studies))
                                    .Single();
            var enrollment = _dbContext.Enrollment
                                        .Where(e => e.IdStudy == studies.IdStudy && e.Semester == 1)
                                        .SingleOrDefault();


            int idEnrollment;
            if (enrollment == null)
            {
                idEnrollment = _dbContext.Enrollment.Count();
                var e = new Enrollment
                {
                    IdEnrollment = idEnrollment,
                    Semester = 1,
                    IdStudy = studies.IdStudy,
                    StartDate = DateTime.Now
                };

                _dbContext.Enrollment.Add(e);
                _dbContext.SaveChanges();
            }
            else
            {
                idEnrollment = enrollment.IdEnrollment;
            }

            var st = new Student
            {
                IndexNumber = request.IndexNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                IdEnrollment = idEnrollment

            };
            _dbContext.Student.Add(st);
            _dbContext.SaveChanges();
            response = new EnrollStudentResponse
            {
                LastName = st.LastName,
                Semester = enrollment.Semester,
                StartDate = enrollment.StartDate.Value
            };
            return response;
        }

        public GetStudentsResponse GetStudents()
        {
            GetStudentsResponse response = new GetStudentsResponse();
            response.Students = _dbContext.Student.ToList();
            return response;
        }

        public void ModifyStudent(ModifyStudentRequest request)
        {
            var st = new Student
            {
                IndexNumber = request.IndexNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                IdEnrollment = request.IdEnrollment,

            };
            _dbContext.Attach(st);
            _dbContext.Entry(st).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public PromoteStudentsResponse PromoteStudents(PromoteStudentRequest request)
        {
            PromoteStudentsResponse response = new PromoteStudentsResponse();
            List<Student> listOfSt = new List<Student>();

            var studies = _dbContext.Studies
                                        .Where(s => s.Name.Equals(request.Studies))
                                        .Single();
            var OldEnrollment = _dbContext.Enrollment
                                        .Where(e => e.IdStudy == studies.IdStudy && e.Semester == request.Semester)
                                        .Single();
            var enrollment = _dbContext.Enrollment
                                        .Where(e => e.IdStudy == studies.IdStudy && e.Semester == request.Semester + 1)
                                        .SingleOrDefault();
            int idEnrollment;
            if (enrollment == null)
            {
                idEnrollment = _dbContext.Enrollment.Count() + 1;
                var enr = new Enrollment
                {
                    IdEnrollment = idEnrollment,
                    Semester = request.Semester + 1,
                    IdStudy = studies.IdStudy,
                    StartDate = DateTime.Now
                };

                _dbContext.Enrollment.Add(enr);
                _dbContext.SaveChanges();
            }
            else
            {
                idEnrollment = enrollment.IdEnrollment;
            }

            var students = _dbContext.Student
                                        .Where(s => s.IdEnrollment == OldEnrollment.IdEnrollment)
                                        .ToList();
            foreach (Student s in students)
            {
                s.IdEnrollment = idEnrollment;
                _dbContext.SaveChanges();
                var s1 = new Student
                {
                    IndexNumber = s.IndexNumber,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    BirthDate = s.BirthDate,
                    IdEnrollment = idEnrollment,

                };
                listOfSt.Add(s1);

            }

            response.Students = listOfSt;


            return response;





        }
    }

}

