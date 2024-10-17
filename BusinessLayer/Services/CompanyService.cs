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
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CompanyService> _logger;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper, ILogger<CompanyService> logger)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<CompanyInfo>> GetAllCompanies()
        {
            try
            {
                _logger.LogInformation("Fetching all companies");
                var res = await _companyRepository.GetAll();
                return _mapper.Map<IEnumerable<CompanyInfo>>(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all companies");
                throw;
            }
        }

        public async Task<CompanyInfo> GetCompanyByCode(string companyCode)
        {
            try
            {
                _logger.LogInformation($"Fetching company: {0}", companyCode);
                var result = await _companyRepository.GetByCode(companyCode);
                return _mapper.Map<CompanyInfo>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching company: {0}", companyCode);
                throw;
            }
        }

        public async Task<bool> CreateUpdateCompany(CompanyInfo companyInfo)
        {
            try
            {
                if (companyInfo == null)
                {
                    _logger.LogWarning("Company data provided is null. Cannot create or update company");
                    return false;
                }

                _logger.LogInformation($"Creating or updating company with code: {0}", companyInfo.CompanyCode);
                var company = _mapper.Map<Company>(companyInfo);
                var result = await _companyRepository.SaveCompany(company);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while creating or updating company with code: {0}", companyInfo?.CompanyCode);
                throw;
            }
        }

        public async Task<bool> DeleteCompany(string siteId, string companyCode)
        {
            try
            {
                _logger.LogInformation($"Deleting company with siteId: {0} and companyCode: {1}", siteId, companyCode);
                var result = await _companyRepository.DeleteCompany(siteId, companyCode);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while deleting company with siteId: {0} and companyCode: {1}", siteId, companyCode);
                throw;
            }
        }
    }
}
