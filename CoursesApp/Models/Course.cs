/////////////////////////////////////////////////////////////////
// Course.cs - Models for CourseApplication Demo               //
//                                                             //
// Jim Fawcett, CSE686 - Internet Programming, Spring 2019     //
/////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesApp.Models
{
  public class Course
  {
    public int CourseId { get; set; }
    public string Identifier { get; set; }
    public string InstituionName { get; set; }

        public string Peroid { get; set; }

        public ICollection<Lecture> Lectures { get; set; }
  }

  public class Lecture
  {
        [Key]
        public int ClassProjectId { get; set; }
    public string ProjectName { get; set; }
    public string Content { get; set; }

    public int? CourseId { get; set; }
    public Course Course { get; set; }
  }

    public class MyBio
    {
        
        public int MyBioId { get; set; }
        public string Bio { get; set; }
    
    }

    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }
        public string Name { get; set; }
    
        public string CommentText { get; set; }

    }


}
