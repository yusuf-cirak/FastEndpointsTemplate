using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Common
{
    public class Entity
    {
        public int Id { get; set; }

        public Entity()
        {
        }

        public Entity(int id) : this()
        {
            Id = id;
        }
    }
}