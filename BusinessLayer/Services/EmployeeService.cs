using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System;
using DataAccessLayer.Model.Models;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployees()
        {
            try
            {
                _logger.LogInformation("Fetching all employees");
                var res = await _employeeRepository.GetAll();
                return _mapper.Map<IEnumerable<EmployeeInfo>>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all employees");
                throw;
            }
        }

        public async Task<EmployeeInfo> GetEmployeeByCode(string employeeCode)
        {
            try
            {
                _logger.LogInformation($"Fetching employee: {0}", employeeCode);
                var result = await _employeeRepository.GetByCode(employeeCode);
                return _mapper.Map<EmployeeInfo>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching employee: {0}", employeeCode);
                throw;
            }
        }

        public async Task<bool> CreateUpdateEmployee(EmployeeInfo employeeInfo)
        {
            try
            {
                if (employeeInfo == null)
                {
                    _logger.LogWarning($"Employee data provided is null. Cannot create or update employee");
                    return false;
                }

                _logger.LogInformation($"Creating or updating employee with code: {0}", employeeInfo.EmployeeCode);
                var employee = _mapper.Map<Employee>(employeeInfo);
                var result = await _employeeRepository.SaveEmployee(employee);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while creating or updating employee with code: {0}", employeeInfo?.EmployeeCode);
                throw;
            }
        }
    }
}

