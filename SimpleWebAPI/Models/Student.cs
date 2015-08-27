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
        /// The Social Security number of a student. 
        /// Example: 3012938847
        /// </summary>
        int SSN { get; set; }


        /// <summary>
        ///  The name of a student
        ///  Example: "Gunnar Atlason"
        /// </summary>
        String Name { get; set; }
	}
}