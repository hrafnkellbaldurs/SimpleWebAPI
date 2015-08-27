using SimpleWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace SimpleWebAPI.Controllers
{
    [RoutePrefix("api/courses")]
    public class CoursesController : ApiController
    {
        private List<Course> _courses;
        private List<Student> _students;

        public CoursesController()
        {
            if(_courses == null)
            {
                _courses = new List<Course>
                {
                    new Course
                    {
                        ID         = 0,
                        Name       = "Web services",
                        TemplateID = "T-514-VEFT",
                        StartDate  = DateTime.Now,
                        EndDate    = DateTime.Now.AddMonths(3),
                        Students   = new List<Student>
                                    {
                                        new Student
                                        {
                                            SSN = "3012932249",
                                            Name = "Hrafnkell Baldursson"
                                        },
                                        new Student
                                        {
                                            SSN = "1212881234",
                                            Name = "Rannveig Guðmundsdóttir"
                                        }
                                    }

                    }
                };

            }
            

        }

        // api/courses
        [HttpGet]
        [Route("")]
        public List<Course> GetCourses()
        {
            //200 request was successful
            //204 no content
            //
            return _courses;
        }

        // api/courses/{id}/students
        [HttpGet]
        [Route("{id}/students")]
        public List <Student> GetStudentsInCourse(int id)
        {
            //200 request was successful
            //204 no content
            //404 id doesnt exist

            //find right course with id
            return _students;
        }

        // api/courses/
        [HttpPost]
        [Route("{id}")]
        [ResponseType(typeof(Course))]
        public IHttpActionResult AddCourse(String name, String templateId)
        {
            //201 creation was successful
            //400 bad request, wrong parameters?
            
            //creating a new course from parameters
            var course = new Course();
            course.Name = name;
            course.TemplateID = templateId;
            course.ID = _courses.Count;
            course.StartDate = DateTime.Now;
            course.EndDate = DateTime.Now.AddMonths(3);
            course.Students = new List<Student> { };

            //setting location url
            var location = Url.Link("GetCourse", new { id = course.ID });

            //adding course to list
            _courses.Add(course);

            return Created(location, course);
        }

        // api/courses/
        [HttpPatch]
        [Route("")]
        public void UpdateCourse(int id)
        { 
            //201 patch successful
            //404 id not found
            //412 parameters wrong?

            //update right course
        }

        // api/courses/
        [HttpDelete]
        [Route("")]
        public void DeleteCourse(int id)
        {
            //200 request successful
            //404 id not found

            //delete right course
        }

        // api/courses/{id}
        [HttpGet]
        [Route("{id:int}", Name ="GetCourse")]
        public Course GetCourseById(int id)
        {
            //200 request successful
            //202 no content
            //404 id not found

            foreach (Course c in _courses)
            {
                if (c.ID == id) return c; 
            }

            //return 404
            throw new HttpResponseException(HttpStatusCode.NotFound);

        }

        // api/courses/
        [HttpPost]
        [Route("")]
        public void AddStudent(Student s, int id)
        {
            //201 post successful
            //
            //404 id not found

            //add student to right course
            
        }



    }
}
