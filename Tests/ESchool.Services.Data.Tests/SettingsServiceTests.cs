﻿namespace ESchool.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ESchool.Data;
    using ESchool.Data.Common.Repositories;
    using ESchool.Data.Models;
    using ESchool.Data.Repositories;

    using Microsoft.EntityFrameworkCore;

    using Moq;

    using Xunit;

    public class SettingsServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var repository = new Mock<ESchool.Data.Common.Repositories.EfDeletableEntityRepository<Setting>>();
            repository.Setup(r => r.All()).Returns(new List<Setting>
                                                        {
                                                            new Setting(),
                                                            new Setting(),
                                                            new Setting(),
                                                        }.AsQueryable());
            var service = new SettingsService(repository.Object);
            Assert.Equal(3, service.GetCount());
            repository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectNumberUsingDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "SettingsTestDb").Options;
            var dbContext = new ApplicationDbContext(options);
            dbContext.Settings.Add(new Setting());
            dbContext.Settings.Add(new Setting());
            dbContext.Settings.Add(new Setting());
            await dbContext.SaveChangesAsync();

            var repository = new ESchool.Data.Repositories.EfDeletableEntityRepository<Setting>(dbContext);
            var service = new SettingsService(repository);
            Assert.Equal(3, service.GetCount());
        }
    }
}
