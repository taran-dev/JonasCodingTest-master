using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using BusinessLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }
        // GET api/employee
        public async Task<IEnumerable<EmployeeDto>> GetAll()
        {
            var items = await _employeeService.GetAllEmployees();
            return _mapper.Map<IEnumerable<EmployeeDto>>(items);
        }

        // GET api/employee/5
        public async Task<EmployeeDto> Get(string employeeCode)
        {
            var item = await _employeeService.GetEmployeeByCode(employeeCode);
            return _mapper.Map<EmployeeDto>(item);
        }

        // POST api/employee
        public async Task<bool> Post([FromBody] EmployeeInfo value)
        {
            var result = await _employeeService.CreateUpdateEmployee(value);
            return result;
        }

        // PUT api/employee/5
        public async Task<bool> Put(int id, [FromBody] EmployeeInfo value)
        {
            var result = await _employeeService.CreateUpdateEmployee(value);
            return result;
        }

    }
}