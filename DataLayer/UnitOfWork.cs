﻿using DataLayer.Repositories;

namespace DataLayer
{
    public class UnitOfWork
    {
        public StudentsRepository Students { get; }

        public ClassRepository Classes { get; }

        public GradeRepository Grades { get; }

        public UsersRepository Users { get; }

        private readonly AppDbContext _dbContext;

        public UnitOfWork
        (
            AppDbContext dbContext,
            StudentsRepository studentsRepository,
            ClassRepository classes,
            GradeRepository grades,
            UsersRepository users
        )
        {
            _dbContext = dbContext;
            Students = studentsRepository;
            Classes = classes;
            Grades = grades;
            Users = users;
        }

        public void SaveChanges()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch(Exception exception)
            {
                var errorMessage = "Error when saving to the database: "
                    + $"{exception.Message}\n\n"
                    + $"{exception.InnerException}\n\n"
                    + $"{exception.StackTrace}\n\n";

                Console.WriteLine(errorMessage);
            }
        }
    }
}