using LearningPlatform.Models.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearningPlatform.Providers
{
    public class CourseProvider
    {
        public List<Course> GetCourses()
        {
            var featuredCourses = GetFeaturedCourses();
            var newCourses = GetNewCourses();

            var courses = new List<Course>();
            courses.AddRange(featuredCourses);
            courses.AddRange(newCourses);

            return courses;
        }

        public List<Course> GetNewCourses()
        {
            return GetAllCourses().Where(c => c.Featured == false).ToList();
        }

        public List<Course> GetFeaturedCourses()
        {
            return GetAllCourses().Where(c => c.Featured == true).ToList();
        }

        public List<Course> GetAllCourses()
        {
            var courses = new List<Course>()
            {
                new Course()
                {
                    Title = ".net Framework. C# fundamentals.",
                    Author = "",
                    Description = "",
                    Featured = true,
                    PublishDate = new DateTime(2016, 11, 20),
                    Tags = new List<string>(){"C#", "OOP"}
                },
                new Course()
                {
                    Title = "Object Oriented Programing. Java fundamentals.",
                    Author = "",
                    Description = "",
                    Featured = false,
                    PublishDate = new DateTime(2016, 5, 25),
                    Tags = new List<string>(){"OOP"}
                },
                new Course()
                {
                    Title = "Procedural programming.",
                    Author = "",
                    Description = "",
                    Featured = true,
                    PublishDate = new DateTime(2016, 1, 11),
                    Tags = new List<string>(){"PL"}
                },
                new Course()
                {
                    Title = "C++ used in the OO way.",
                    Author = "",
                    Description = "",
                    Featured = false,
                    PublishDate = new DateTime(2016, 10, 11),
                    Tags = new List<string>(){"C++", "OOP"}
                },
                new Course()
                {
                    Title = "Python advanced aspects.",
                    Author = "",
                    Description = "",
                    Featured = true,
                    PublishDate = new DateTime(2015, 10, 11),
                    Tags = new List<string>(){ "Python" }
                },
                new Course()
                {
                    Title = "Javascript fundamentals.",
                    Author = "",
                    Description = "",
                    Featured = false,
                    PublishDate = new DateTime(2017, 2, 2),
                    Tags = new List<string>(){ "Javascript" }
                },
                new Course()
                {
                    Title = "Functional Programming aspects. Prolog.",
                    Author = "",
                    Description = "",
                    Featured = true,
                    PublishDate = new DateTime(2017, 4, 2),
                    Tags = new List<string>(){ "Prolog" }
                },
                new Course()
                {
                    Title = "Haskell for begginers.",
                    Author = "",
                    Description = "",
                    Featured = true,
                    PublishDate = new DateTime(2017, 4, 12),
                    Tags = new List<string>(){ "Haskell", "PL" }
                },
                new Course()
                {
                    Title = "Ruby fundamentals.",
                    Author = "",
                    Description = "",
                    Featured = false,
                    PublishDate = new DateTime(2014, 4, 12),
                    Tags = new List<string>(){ "Ruby" }
                },
                new Course()
                {
                    Title = "HTML and CSS advanced.",
                    Author = "",
                    Description = "",
                    Featured = true,
                    PublishDate = new DateTime(2014, 11, 12),
                    Tags = new List<string>(){ "HTML", "CSS" }
                },
                new Course()
                {
                    Title = "HTML and CSS advanced.",
                    Author = "",
                    Description = "",
                    Featured = false,
                    PublishDate = new DateTime(2014, 11, 12),
                    Tags = new List<string>(){ "HTML", "CSS" }
                },

                new Course()
                {
                    Title = ".nnet Framework. C# fundamentals.",
                    Author = "",
                    Description = "",
                    Featured = true,
                    PublishDate = new DateTime(2016, 11, 20),
                    Tags = new List<string>(){"C#", "OOP"}
                },
                new Course()
                {
                    Title = "OObject Oriented Programing. Java fundamentals.",
                    Author = "",
                    Description = "",
                    Featured = false,
                    PublishDate = new DateTime(2016, 5, 25),
                    Tags = new List<string>(){"OOP"}
                },
                new Course()
                {
                    Title = "PProcedural programming.",
                    Author = "",
                    Description = "",
                    Featured = false,
                    PublishDate = new DateTime(2016, 1, 11),
                    Tags = new List<string>(){"PL"}
                },
                new Course()
                {
                    Title = "CC++ used in the OO way.",
                    Author = "",
                    Description = "",
                    Featured = true,
                    PublishDate = new DateTime(2016, 10, 11),
                    Tags = new List<string>(){"C++", "OOP"}
                },
                new Course()
                {
                    Title = "PPython advanced aspects.",
                    Author = "",
                    Description = "",
                    Featured = false,
                    PublishDate = new DateTime(2015, 10, 11),
                    Tags = new List<string>(){ "Python" }
                },
                new Course()
                {
                    Title = "JJavascript fundamentals.",
                    Author = "",
                    Description = "",
                    Featured = true,
                    PublishDate = new DateTime(2017, 2, 2),
                    Tags = new List<string>(){ "Javascript" }
                },
                new Course()
                {
                    Title = "FFunctional Programming aspects. Prolog.",
                    Author = "",
                    Description = "",
                    Featured = true,
                    PublishDate = new DateTime(2017, 4, 2),
                    Tags = new List<string>(){ "Prolog" }
                },
                new Course()
                {
                    Title = "HHaskell for begginers.",
                    Author = "",
                    Description = "",
                    Featured = false,
                    PublishDate = new DateTime(2017, 4, 12),
                    Tags = new List<string>(){ "Haskell", "PL" }
                },
                new Course()
                {
                    Title = "RRuby fundamentals.",
                    Author = "",
                    Description = "",
                    Featured = true,
                    PublishDate = new DateTime(2014, 4, 12),
                    Tags = new List<string>(){ "Ruby" }
                },
                new Course()
                {
                    Title = "HHTML and CSS advanced.",
                    Author = "",
                    Description = "",
                    Featured = false,
                    PublishDate = new DateTime(2014, 11, 12),
                    Tags = new List<string>(){ "HTML", "CSS" }
                },

                new Course()
                {
                    Title = ".NNnet Framework. C# fundamentals.",
                    Author = "",
                    Description = "",
                    Featured = false,
                    PublishDate = new DateTime(2016, 11, 20),
                    Tags = new List<string>(){"C#", "OOP"}
                },
                new Course()
                {
                    Title = "OOObject Oriented Programing. Java fundamentals.",
                    Author = "",
                    Description = "",
                    Featured = false,
                    PublishDate = new DateTime(2016, 5, 25),
                    Tags = new List<string>(){"OOP"}
                },
                new Course()
                {
                    Title = "PPProcedural programming.",
                    Author = "",
                    Description = "",
                    Featured = false,
                    PublishDate = new DateTime(2016, 1, 11),
                    Tags = new List<string>(){"PL"}
                },
                new Course()
                {
                    Title = "CCC++ used in the OO way.",
                    Author = "",
                    Description = "",
                    Featured = true,
                    PublishDate = new DateTime(2016, 10, 11),
                    Tags = new List<string>(){"C++", "OOP"}
                },
                new Course()
                {
                    Title = "PPPython advanced aspects.",
                    Author = "",
                    Description = "",
                    Featured = false,
                    PublishDate = new DateTime(2015, 10, 11),
                    Tags = new List<string>(){ "Python" }
                },
                new Course()
                {
                    Title = "JJJavascript fundamentals.",
                    Author = "",
                    Description = "",
                    Featured = true,
                    PublishDate = new DateTime(2017, 2, 2),
                    Tags = new List<string>(){ "Javascript" }
                },
                new Course()
                {
                    Title = "FFFunctional Programming aspects. Prolog.",
                    Author = "",
                    Description = "",
                    Featured = false,
                    PublishDate = new DateTime(2017, 4, 2),
                    Tags = new List<string>(){ "Prolog" }
                },
                new Course()
                {
                    Title = "HHHaskell for begginers.",
                    Author = "",
                    Description = "",
                    Featured = true,
                    PublishDate = new DateTime(2017, 4, 12),
                    Tags = new List<string>(){ "Haskell", "PL" }
                },
                new Course()
                {
                    Title = "RRRuby fundamentals.",
                    Author = "",
                    Description = "",
                    Featured = true,
                    PublishDate = new DateTime(2014, 4, 12),
                    Tags = new List<string>(){ "Ruby" }
                },
                new Course()
                {
                    Title = "HHHTML and CSS advanced.",
                    Author = "",
                    Description = "",
                    Featured = false,
                    PublishDate = new DateTime(2014, 11, 12),
                    Tags = new List<string>(){ "HTML", "CSS" }
                },
                new Course()
                {
                    Title = "HHHTML and CSS advanced.",
                    Author = "",
                    Description = "",
                    Featured = true,
                    PublishDate = new DateTime(2014, 11, 12),
                    Tags = new List<string>(){ "HTML", "CSS" }
                },
            };

            var index = 1000;
            foreach(Course c in courses)
            {
                c.Id = index;
                index++;
            }

            return courses;
        }
    }
}