using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }
        // GET api/<controller>
        public async Task<IEnumerable<CompanyDto>> GetAll()
        {
            var items = await _companyService.GetAllCompanies();
            return _mapper.Map<IEnumerable<CompanyDto>>(items);
        }

        // GET api/<controller>/5
        public async Task<CompanyDto> Get(string companyCode)
        {
            var item = await _companyService.GetCompanyByCode(companyCode);
            return _mapper.Map<CompanyDto>(item);
        }

        // POST api/<controller>
        public async Task<bool> Post([FromBody]CompanyInfo value)
        {
            var result = await _companyService.CreateUpdateCompany(value);
            return result;
        }

        // PUT api/<controller>/5
        public async Task<bool> Put(int id, [FromBody] CompanyInfo value)
        {
            var result = await _companyService.CreateUpdateCompany(value);
            return result;
        }

        // DELETE api/<controller>/5
        public async Task<bool> Delete(string siteId, string companyCode)
        {
            var result = await _companyService.DeleteCompany(siteId, companyCode);
            return result;
        }
    }
}