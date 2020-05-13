using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cw10.DTOs.Request;
using cw10.Models;
using cw10.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace cw10.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly IStudentsDbService _dbContext;
        public StudentsController(IStudentsDbService context) => _dbContext = context;

        [HttpGet]
        public IActionResult GetStudents()
        {
            IActionResult response;
            try
            {
                response = Ok(_dbContext.GetStudents());
            }
            catch (Exception exc)
            {
                response = BadRequest(exc.StackTrace);
            }

            return response;
        }
        [HttpPost]
        public IActionResult ModifyStudent(ModifyStudentRequest request)
        {
            IActionResult response;
            try
            {
                _dbContext.ModifyStudent(request);
                response = Ok(request.IndexNumber);
            }
            catch (Exception exc)
            {
                response = BadRequest( exc.StackTrace);
            }

            return response;
        }
        [HttpDelete]
        public IActionResult DeleteStudent(DeleteStudentRequest request)
        {
            IActionResult response;
            try
            {
                _dbContext.DeleteStudent(request);
                response = Ok(request.IndexNumber);
            }
            catch (Exception exc)
            {
                response = BadRequest( exc.StackTrace);
            }

            return response;
        }
        [HttpPost("enroll")]
        public IActionResult EnrollStudents(EnrollStudentRequest request)
        {
            IActionResult response;
            try
            {
                response = Ok(_dbContext.EnrollStudent(request));
            }
            catch (Exception exc)
            {
                response = BadRequest(exc.StackTrace);
            }

            return response;
        }
        [HttpPost("promote")]
        public IActionResult PromoteStudents(PromoteStudentRequest request)
        {
            IActionResult response;
            try
            {
                response = Ok(_dbContext.PromoteStudents(request));
            }
            catch (Exception exc)
            {
                response = BadRequest(exc.StackTrace);
            }

            return response;
        }
    }

}
