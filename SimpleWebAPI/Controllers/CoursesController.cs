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
        private static List<Course> _courses;

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
            return _courses;
        }

        // api/courses/{id}/students
        [HttpGet]
        [Route("{id:int}/students")]
        public List <Student> GetStudentsInCourse(int id)
        {
            // If there are no courses in the list
            if (_courses.Count == 0) throw new HttpResponseException(HttpStatusCode.NoContent);

            // Search courses list for the given course
            foreach (Course c in _courses)
            {
                if (c.ID == id)
                {
                    return c.Students;
                }
            }

            //return 404 if the course is not found
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // api/courses/
        [Route("")]
        [ResponseType(typeof(Course))]
        [HttpPost]
        public IHttpActionResult AddCourse(Course c)
        {
            if(c == null)
            {
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
            }

            //setting location url
            var location = Url.Link("GetCourse", new { id = c.ID });

            //adding course to list
            _courses.Add(c);

            return Created(location, c);    
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
        [Route("{id:int}")]
        public IHttpActionResult DeleteCourse(int id)
        {
            //200 request successful
            //404 id not found

            foreach (Course c in _courses)
            {
                if (c.ID == id)
                {
                    _courses[_courses.IndexOf(c)] = null;
                    throw new HttpResponseException(HttpStatusCode.NoContent);
                }
            }

            //return 404 if course to be deleted is not found
            return NotFound();
        }

        // api/courses/{id}
        [HttpGet]
        [Route("{id:int}", Name ="GetCourse")]
        public Course GetCourseById(int id)
        {
            foreach (Course c in _courses)
            {
                if (c.ID == id) return c; 
            }

            //return 404
            throw new HttpResponseException(HttpStatusCode.NotFound);

        }

        // api/courses/
        [HttpPost]
        [Route("{id:int}/{ssn}/{name}")]
        public IHttpActionResult AddStudent(int id, String ssn, String name)
        {
            foreach (Course c in _courses)
            {
                if (c.ID == id) {
                    var s = new Student();
                    s.SSN = ssn;
                    s.Name = name;
                    _courses[_courses.IndexOf(c)].Students.Add(s);

                    //setting location url
                    var location = Url.Link("GetStudent", new { SSN = s.SSN });

                    return Created(location, s);
                } 
            }

            //return 404
            return NotFound();
        }



    }
}
