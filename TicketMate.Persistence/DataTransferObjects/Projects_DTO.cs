﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketMate.Persistence.DataTransferObjects
{
    public class Projects_DTO
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
