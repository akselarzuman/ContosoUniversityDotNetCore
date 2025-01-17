﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using ContosoUniversity.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Features.Departments
{
    public class Index
    {
        public class Query : IRequest<List<Model>>
        {
        }

        public class Model
        {
            public string Name { get; set; }

            public decimal Budget { get; set; }

            public DateTime StartDate { get; set; }

            public int Id { get; set; }

            [Display(Name = "Administrator")]
            public string AdministratorFullName { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, List<Model>>
        {
            private readonly SchoolContext _context;

            public QueryHandler(SchoolContext context) => _context = context;

            public async Task<List<Model>> Handle(Query message, CancellationToken cancellationToken)
            {
                var projectTo = _context.Departments
                    .ProjectTo<Model>();
                return await projectTo.ToListAsync();
            }
        }
    }
}