using LearningPlatform.Core.Entities;
using LearningPlatform.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArusTest
{
	public class Program
	{
		static void Main(string[] args)
		{
			var repo = new DataRepository<User>(Environment.GetEnvironmentVariable("AWP_DB"));

			var all = repo.GetAll();

			var justAFew = repo.GetById(2);

			var coursesRepo = new DataRepository<Course>(Environment.GetEnvironmentVariable("AWP_DB"));

			var onlysome = coursesRepo.GetByProperty("Name", "Radical American Symbols Since 1881");
		}
	}
}
