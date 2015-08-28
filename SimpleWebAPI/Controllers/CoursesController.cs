using SimpleWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Linq;

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
                                            SSN = "1212882659",
                                            Name = "Rannveig Guðmundsdóttir"
                                        }
                                    }

                    },
                    new Course
                    {
                        ID         = 1,
                        Name       = "Computer Networking",
                        TemplateID = "T-409-TSAM",
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
                                            SSN = "1212882659",
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
            foreach (Course c in _courses)
            {
                if (c.ID == id)
                {
                    return c.Students;
                }
            }

            //return 404
            throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // api/courses/
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(Course))]
        public IHttpActionResult AddCourse(Course c)
        {
            //checking if the course being added is not of the right data type
            if (c == null)
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
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateCourse(int id, Course course)
        {
            //checking if the course being added is not of the right data type
            if (course == null)
            {
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
            }

            //update right course
            foreach (Course c in _courses)
            {
                if (c.ID == id)
                {
                    var temp = _courses.SingleOrDefault(x=>x.ID == course.ID);
                    temp.Name = course.Name;
                    temp.StartDate = course.StartDate;
                    temp.EndDate = course.EndDate;
                    temp.Students = course.Students;

                    //201 patch successful
                    var location = Url.Link("GetCourse", new { id = course.ID });
                    return Created(location, temp);

                }
            }

            //404 id not found
            return NotFound();
        }

        // api/courses/
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteCourse(int id)
        {
            foreach (Course c in _courses)
            {
                if (c.ID == id)
                {
                    _courses.Remove(c);
                    throw new HttpResponseException(HttpStatusCode.NoContent);
                }
            }

            //return 404
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
        [Route("{id:int}/students")]
        public IHttpActionResult AddStudentToCourse(int id, Student student)
        {
            //checking if the student being added is not of the right data type
            if (student == null)
            {
                throw new HttpResponseException(HttpStatusCode.PreconditionFailed);
            }

            foreach (Course c in _courses)
            {
                if (c.ID == id) {
                    _courses[_courses.IndexOf(c)].Students.Add(student);
                    var location = Url.Link("GetCourse", new { id = c.ID });
                    return Created(location, student);
                } 
            }

            //return 404
            return NotFound();
        }



    }
}
