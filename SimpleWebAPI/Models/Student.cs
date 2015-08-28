using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleWebAPI.Models
{
	/// <summary>
    /// This class represents a single student at a school
    /// </summary>
    public class Student
	{
        /// <summary>
        /// The Social Security Number of a student. 
        /// Used as a unique identifier.
        /// Example: "3012933419"
        /// </summary>
        public String SSN { get; set; }

        /// <summary>
        ///  The name of a student
        ///  Example: "Gunnar Atlason"
        /// </summary>
        public String Name { get; set; }
	}
}