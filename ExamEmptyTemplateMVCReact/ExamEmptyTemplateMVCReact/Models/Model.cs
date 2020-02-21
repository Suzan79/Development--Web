using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace SimpleModelsAndRelations.Model
{
    public class SomeContext : DbContext
    {
        public SomeContext(DbContextOptions<SomeContext> options) : base(options){ }
    }

}