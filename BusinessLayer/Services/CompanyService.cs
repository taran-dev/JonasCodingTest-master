using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;
using System.Globalization;

namespace BusinessLayer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CompanyInfo>> GetAllCompanies()
        {
            var res = await _companyRepository.GetAll();
            return _mapper.Map<IEnumerable<CompanyInfo>>(res);
        }

        public async Task<CompanyInfo> GetCompanyByCode(string companyCode)
        {
            var result = await _companyRepository.GetByCode(companyCode);
            return _mapper.Map<CompanyInfo>(result);
        }

        public async Task<bool> CreateUpdateCompany(CompanyInfo companyInfo)
        {
            if(companyInfo == null)
            {
                return false;
            }

            var company = _mapper.Map<Company>(companyInfo);
            var result = await _companyRepository.SaveCompany(company);
            return result;
        }

        public async Task<bool> DeleteCompany(string siteId, string companyCode)
        {
            var result = await _companyRepository.DeleteCompany(siteId, companyCode);
            return result;
        }
    }
}
