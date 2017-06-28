using QDot.Core.Service;
using QDot.Infraestructure.Models;
using QDot.Infraestructure.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using QDot.API.Client.BaseAPI;
using QDot.API.Client.QDotAPI.Requests;
using FluentAssertions;
using System;
using System.Threading.Tasks;

namespace QDot.Core.UnitTest.Service
{
    public class DeveloperServiceTest
    {
        #region GetAll Tests
        [Fact(DisplayName = "Get all with one developer")]
        public async void GetAllWithOneDeveloperWithSkills()
        {
            //Arrange
            var mockRepository = new Mock<IRepository<Developer>>();
            var developerService = new DeveloperService(mockRepository.Object, null);
            var mockDeveloper = new Developer
            {
                FirstName = "Colin",
                LastName = "Abbot",
                Age = 34,
                Skills = new List<Skill>()
            };
            mockRepository.Setup(m => m.GetAllAsync()).ReturnsAsync(new List<Developer>
            {
                mockDeveloper
            });

            //Act
            var developers = await developerService.GetAllAsync();

            //Assert
            developers.Should().HaveCount(1);
            developers.First().ShouldBeEquivalentTo(mockDeveloper);
        }

        [Fact(DisplayName = "Get all with no developers")]
        public async void GetAllWithNoDevelopers()
        {
            //Arrange
            var mockRepository = new Mock<IRepository<Developer>>();
            var developerService = new DeveloperService(mockRepository.Object, null);
            mockRepository.Setup(m => m.GetAllAsync()).ReturnsAsync(new List<Developer>());

            //Act
            var developers = await developerService.GetAllAsync();

            //Assert
            developers.Should().BeEmpty();
        }

        #endregion

        #region GetAllAboveSkillScore Tests

        [Fact(DisplayName = "Get developers with too high skill score")]
        public async void GetDevelopersWithTooHighSkillScore()
        {
            //Arrange
            var mockAPIClient = new Mock<IAPIClient>();
            var developerService = new DeveloperService(null, mockAPIClient.Object);
            mockAPIClient.Setup(m => m.Execute(It.IsAny<GetAllDevelopersRequest>())).ReturnsAsync(new List<Developer>
            {
                new Developer
                {
                    FirstName = "Colin",
                    LastName = "Abbot",
                    Age = 34,
                    Skills = _GetLowBackendSkills()
                },
                new Developer
                {
                    FirstName = "Sarah",
                    LastName = "Winchester",
                    Age = 40,
                    Skills = _GetLowFrontendSkills()
                }
            });

            //Act
            var developers = await developerService.GetDevelopersBySkillAsync(12);

            //Assert
            developers.Should().BeEmpty();
        }

        [Fact(DisplayName = "Get developers skills' types just with high score")]
        public async void GetDevelopersSkillTypesJustWithHighScore()
        {
            //Arrange
            var mockAPIClient = new Mock<IAPIClient>();
            var developerService = new DeveloperService(null, mockAPIClient.Object);
            var mixedSkills = _GetHighBackendSkills();
            mixedSkills.AddRange(_GetLowFrontendSkills());
            mockAPIClient.Setup(m => m.Execute(It.IsAny<GetAllDevelopersRequest>())).ReturnsAsync(new List<Developer>
            {
                new Developer
                {
                    FirstName = "Colin",
                    LastName = "Abbot",
                    Age = 34,
                    Skills = mixedSkills
                }
            });

            //Act
            var developers = await developerService.GetDevelopersBySkillAsync(10);

            //Assert
            developers.Should().HaveCount(1);
            developers.SelectMany(dev => dev.Skills.Select(skill => skill.Type)).Should().OnlyContain(x => x.Equals("backend"));
        }

        [Fact(DisplayName = "Throw exception for negative skills")]
        public void ThrowExceptionForNegativeSkills()
        {
            //Arrange
            var developerService = new DeveloperService(null, null);

            //Act
            Func<Task> act = async () => await developerService.GetDevelopersBySkillAsync(-1);
            
            //Assert
            act.ShouldThrow<ArgumentException>();
        }
        #endregion


        #region Arrange Helpers
        private List<Skill> _GetHighBackendSkills()
        {
            return new List<Skill>
            {
                new Skill
                {
                    Level = 10,
                    Name = "C#",
                    Type = "backend"
                },
                new Skill
                {
                    Level = 4,
                    Name = "Entity Framework",
                    Type = "backend"
                },
            };
        }

        private List<Skill> _GetLowBackendSkills()
        {
            return new List<Skill>
            {
                new Skill
                {
                    Level = 3,
                    Name = "C#",
                    Type = "backend"
                },
                new Skill
                {
                    Level = 2,
                    Name = "Entity Framework",
                    Type = "backend"
                },
            };
        }

        private List<Skill> _GetHighFrontendSkills()
        {
            return new List<Skill>
            {
                new Skill
                {
                    Level = 10,
                    Name = "AngularJS",
                    Type = "frontend"
                },
                new Skill
                {
                    Level = 4,
                    Name = "Javascript",
                    Type = "frontend"
                },
            };
        }

        private List<Skill> _GetLowFrontendSkills()
        {
            return new List<Skill>
            {
                new Skill
                {
                    Level = 3,
                    Name = "AngularJS",
                    Type = "frontend"
                },
                new Skill
                {
                    Level = 4,
                    Name = "Javascript",
                    Type = "frontend"
                },
            };
        }
        #endregion
    }
}
