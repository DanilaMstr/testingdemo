using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.IO;
using DemoLibrary.Models;

namespace DemoLibrary.Tests
{
    public class DataAccessTests
    {
        [Fact]
        public void AddPersonsToPeopleList_ShouldWork()
        {
            PersonModel newPerson = new PersonModel { FirstName = "Tim", LastName = "Corey" };
            List<PersonModel> people = new List<PersonModel>();

            DataAccess.AddPersonToPeopleList(people, newPerson);

            Assert.True(people.Count() == 1);
            Assert.Contains(newPerson, people);
        }

        [Theory]
        [InlineData("Tim", "", "LastName")]
        [InlineData("", "Corey", "FirstName")]
        public void AddPersonsToPeopleList_ShouldFail(string firstName, string lastName, string param)
        {
            PersonModel newPerson = new PersonModel { FirstName = firstName, LastName = lastName };
            List<PersonModel> people = new List<PersonModel>();

            Assert.Throws<ArgumentException>(param, () =>
                DataAccess.AddPersonToPeopleList(people, newPerson));
        }

        [Fact]
        public void ConvertModelsToCSV_ShoudWork()
        {
            List<PersonModel> people = new List<PersonModel>();
            people.Add(new PersonModel { FirstName = "Tim", LastName = "Corey" });
            string actual = $"Tim,Corey";

            Assert.Contains(
                actual,
                DataAccess.ConvertModelsToCSV(people));
        }

        [Fact]
        public void ConvertModelsToCSV_ArgumentNullShoudFail()
        {
            List<PersonModel> people = null;

            Assert.Throws<ArgumentNullException>("people", () =>
                DataAccess.ConvertModelsToCSV(people));
        }

        [Fact]
        public void ConvertModelsToCSV_EmptyListShoudFail()
        {
            List<PersonModel> people = new List<PersonModel>();

            Assert.Throws<ArgumentException>("people", () =>
                DataAccess.ConvertModelsToCSV(people));
        }
    }
}
