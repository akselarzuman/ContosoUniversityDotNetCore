﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using ContosoUniversity.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Features.Departments
{
    public class Delete
    {
        public class Query : IRequest<Command>
        {
            public int Id { get; set; }
        }

        public class Command : IRequest
        {
            public string Name { get; set; }

            public decimal Budget { get; set; }

            public DateTime StartDate { get; set; }

            public int Id { get; set; }

            [Display(Name = "Administrator")]
            public string AdministratorFullName { get; set; }

            public byte[] RowVersion { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Command>
        {
            private readonly SchoolContext _db;

            public QueryHandler(SchoolContext db) => _db = db;

            public async Task<Command> Handle(Query message, CancellationToken cancellationToken) => await _db
                .Departments
                .Where(d => d.Id == message.Id)
                .ProjectTo<Command>()
                .SingleOrDefaultAsync();
        }

        public class CommandHandler : AsyncRequestHandler<Command>
        {
            private readonly SchoolContext _db;

            public CommandHandler(SchoolContext db) => _db = db;

            protected override async Task Handle(Command message, CancellationToken cancellationToken)
            {
                var department = await _db.Departments.FindAsync(message.Id);

                _db.Departments.Remove(department);
            }
        }
    }
}