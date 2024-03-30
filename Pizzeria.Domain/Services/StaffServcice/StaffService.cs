using Pizzeria.Domain.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzeria.Domain.Models;

namespace Pizzeria.Domain.Services.StaffServcice
{
    public class StaffService(IStaffRepository staffRepository) 
        : BaseService<Staff>(staffRepository), IStaffService
    {
    }
}
