﻿using CompanyEmployees.Presentation.ActionFilters;
using CompanyEmployees.Presentation.ModelBinders;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/companies")]
[ApiController]
[ApiExplorerSettings(GroupName = "v1")]
//[ResponseCache(CacheProfileName = "120SecondDuration")]
public class CompaniesController : ControllerBase
{
    private readonly IServiceManager _service;

    public CompaniesController(IServiceManager service)
    {
        _service = service;
    }

    /// <summary>
    /// Get the list of all companies
    /// </summary>
    /// <returns>The companies list</returns>
    [HttpGet(Name = "GetCompanies")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> GetCompanies()
    {
        var companies = await _service.CompanyService.GetAllCompaniesAsync(trackChanges: false);

        return Ok(companies);
    }

    [HttpGet("{id:guid}", Name = "CompanyById")]
    [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 60)]
    [HttpCacheValidation(MustRevalidate = false)]
    //[ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetCompany(Guid id)
    {
        var company = await _service.CompanyService.GetCompanyAsync(id, trackChanges: false);

        return Ok(company);
    }

    /// <summary>
    /// Create a newly created company
    /// </summary>
    /// <param name="company"></param>
    /// <returns>A newly created company</returns>
    /// <response code="201">Returns the newly created item</response>
    /// <response code="400">f the item is null</response>
    /// <response code="422">If the model is invalid</response>
    [HttpPost(Name = "CreateCompany")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(422)]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto company)
    {
        var createdCompany = await _service.CompanyService.CreateCompanyAsync(company);

        return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
    }

    [HttpGet("collection/({ids})", Name = "CompanyCollection")]
    public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
    {
        var companies = await _service.CompanyService.GetByIdsAsync(ids, trackChanges: false);

        return Ok(companies);
    }

    [HttpPost("collection")]
    public async Task<IActionResult> CreateCompanyCollectionAsync([FromBody] IEnumerable<CompanyForCreationDto> companyCollection)
    {
        var result = await _service.CompanyService.CreateCompanyCollectionAsync(companyCollection);

        return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companies);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        await _service.CompanyService.DeleteCompanyAsync(id, trackChanges: false);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto company)
    {
        await _service.CompanyService.UpdateCompanyAsync(id, company, trackChanges: true);

        return NoContent();
    }

    [HttpOptions]
    public IActionResult GetCompaniesOptions()
    {
        Response.Headers.Add("Allow", "GET, OPTIONS, POST");

        return Ok();
    }

}

