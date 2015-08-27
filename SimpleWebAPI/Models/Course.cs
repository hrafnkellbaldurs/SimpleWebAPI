using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleWebAPI.Models
{
    /// <summary>
    /// This class represents a single course in a school
    /// </summary>
    public class Course
    {
        /// <summary>
        /// The name of a course. Example: "Web Services".
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// The Template ID of a course. Example: "T-514-VEFT"
        /// </summary>
        public String TemplateID { get; set;  }

        /// <summary>
        /// The ID of a course. Example: 1.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The start date of a course. Example: 2015-08-17
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The end date of a course. Example: 2015-11-08
        /// </summary>
        public DateTime EndDate { get; set; }


        /// <summary>
        /// A list of students enrolled in this class. Example: { new Student{ SSN = 3012937789, Name: = "John Walker"}, new Student{ SSN = 1234842245, Name = "Paul Johnson"} }
        /// </summary>
        public List<Student> Students { get; set; }

    }
}