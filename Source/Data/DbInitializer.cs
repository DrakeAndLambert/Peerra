﻿using System;
using System.Collections.Generic;
using System.Linq;
using DrakeLambert.Peerra.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DrakeLambert.Peerra.Data
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DbInitializer> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly InitialDataOptions _initialData;

        public DbInitializer(ApplicationDbContext context, IOptions<InitialDataOptions> initialData, ILogger<DbInitializer> logger, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _initialData = initialData.Value;
            _logger = logger;
            _userManager = userManager;
        }

        public void Seed()
        {
            _logger.LogInformation("Seeding database using provider: {0}", _context.Database.ProviderName);
            if (_context.Database.IsSqlServer())
            {
                _context.Database.Migrate();
            }

            if (_initialData.Overwrite)
            {
                _context.RoleClaims.RemoveRange(_context.RoleClaims);
                _context.Roles.RemoveRange(_context.Roles);
                _context.UserClaims.RemoveRange(_context.UserClaims);
                _context.UserLogins.RemoveRange(_context.UserLogins);
                _context.UserRoles.RemoveRange(_context.UserRoles);
                _context.UserTokens.RemoveRange(_context.UserTokens);
                _context.UserTopics.RemoveRange(_context.UserTopics);
                _context.HelpRequests.RemoveRange(_context.HelpRequests);
                _context.Issues.RemoveRange(_context.Issues);
                _context.Topics.RemoveRange(_context.Topics);
                _context.Users.RemoveRange(_context.Users);

                _context.SaveChanges();

                _logger.LogInformation("Adding {count} top level topics.", _initialData.Topics.Length);

                _context.Topics.AddRange(_initialData.Topics.Select(io => (Topic)io));
                _context.SaveChanges();

                foreach (var user in _initialData.Users)
                {
                    var applicationUser = new ApplicationUser
                    {
                        UserName = user.UserName,
                        Email = $"{user.UserName}@email.com",
                        EmailConfirmed = true
                    };
                    _userManager.CreateAsync(applicationUser, "Password1!").GetAwaiter().GetResult();
                }

                var random = new Random();
                foreach (var user in _context.Users)
                {
                    var skillCount = random.Next(5, 10);
                    var skillSet = new HashSet<Guid>();
                    for (int i = 0; i < skillCount; i++)
                    {
                        var topic = _context.Topics.OrderBy(iss => iss.Id).Skip(random.Next(0, _context.Topics.Count() - 1)).First();
                        skillSet.Add(topic.Id);
                    }
                    _context.UserTopics.AddRange(skillSet
                        .Select(topicId => new UserTopic { UserId = user.Id, TopicId = topicId })
                    );
                }
                _context.SaveChanges();
            }

        }
    }
}
