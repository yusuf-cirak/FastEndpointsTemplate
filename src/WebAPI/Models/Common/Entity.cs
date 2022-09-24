using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Common
{
    public class Entity
    {


        public string Id { get; set; }

        public Entity()
        {

        }

        public Entity(string id)
        {
            Id = id;
        }
    }
}