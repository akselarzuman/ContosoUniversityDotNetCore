﻿using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ContosoUniversity.Data;
using ContosoUniversity.Models;
using MediatR;

namespace ContosoUniversity.Features.Courses
{
    public class Create
    {
        public class Command : IRequest<int>
        {
            [IgnoreMap]
            public int Number { get; set; }
            public string Title { get; set; }
            public int Credits { get; set; }
            public Department Department { get; set; }
        }

        public class Handler : IRequestHandler<Command, int>
        {
            private readonly SchoolContext _db;

            public Handler(SchoolContext db) => _db = db;

            public async Task<int> Handle(Command message, CancellationToken cancellationToken)
            {
                var course = Mapper.Map<Command, Course>(message);
                course.Id = message.Number;

                _db.Courses.Add(course);

                await _db.SaveChangesAsync();

                return course.Id;
            }
        }
    }
}