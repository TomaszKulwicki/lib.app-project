using System;
using System.Linq;
using LibApp.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LibApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.MembershipTypes.Any() && context.Customers.Any() && context.Books.Any() && context.Roles.Any() && context.Users.Any())
                {
                    Console.WriteLine("Database already seeded");
                    return;
                }

                if (!context.MembershipTypes.Any())
                {
                    context.MembershipTypes.AddRange(
                        new MembershipType
                        {
                            Id = 1,
                            SignUpFee = 0,
                            DurationInMonths = 0,
                            DiscountRate = 0
                        },
                        new MembershipType
                        {
                            Id = 2,
                            SignUpFee = 30,
                            DurationInMonths = 1,
                            DiscountRate = 10
                        },
                        new MembershipType
                        {
                            Id = 3,
                            SignUpFee = 90,
                            DurationInMonths = 3,
                            DiscountRate = 15
                        },
                        new MembershipType
                        {
                            Id = 4,
                            SignUpFee = 300,
                            DurationInMonths = 12,
                            DiscountRate = 20
                        });
                }

                if (!context.Customers.Any())
                {
                    context.Customers.AddRange(
                        new Customer
                        {
                            //Id = 1,
                            Name = "Jarosław Andrzejewski",
                            Birthdate = new DateTime(1971, 5, 31),
                            HasNewsletterSubscribed = false,
                            MembershipTypeId = 1
                        },
                        new Customer
                        {
                            //Id = 2,
                            Name = "Krzysztof Kononowicz",
                            Birthdate = new DateTime(1969, 5, 26),
                            HasNewsletterSubscribed = false,
                            MembershipTypeId = 1
                        },
                        new Customer
                        {
                            //Id = 3,
                            Name = "Wojciech Suchodolski",
                            Birthdate = new DateTime(1978, 11, 10),
                            HasNewsletterSubscribed = false,
                            MembershipTypeId = 1
                        });
                }
                if (!context.Books.Any())
                {
                    context.Books.AddRange(
                    new Book
                    {
                        Name = "Autobiografia",
                        AuthorName = "Jan Kowalski",
                        GenreId = 6,
                        DateAdded = new DateTime(2020, 11, 10),
                        ReleaseDate = new DateTime(2019, 11, 10),
                        NumberInStock = 3,
                        NumberAvailable = 3,
                    },
                    new Book
                    {
                        Name = "Historia Polski",
                        AuthorName = "Jan Kowalski",
                        GenreId = 2,
                        DateAdded = new DateTime(2020, 11, 10),
                        ReleaseDate = new DateTime(2019, 11, 10),
                        NumberInStock = 3,
                        NumberAvailable = 3,
                    },
                    new Book
                    {
                        Name = "C++ dla poczatkujacych",
                        AuthorName = "Jan Kowalski",
                        GenreId = 4,
                        DateAdded = new DateTime(2020, 11, 10),
                        ReleaseDate = new DateTime(2019, 11, 10),
                        NumberInStock = 3,
                        NumberAvailable = 3,
                    }
                    );

                }

                context.SaveChanges();
            }
            SeedUsers.Seed(userManager, roleManager);
        }
    }
}