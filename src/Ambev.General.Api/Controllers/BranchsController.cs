﻿using Ambev.Core.Application.Branch.GetBranchById.GetBranchById;
using Ambev.Core.Application.UseCases.Commands.Branch.DeleteBranch;
using Ambev.Core.Application.UseCases.Commands.Branch.UpdateBranch;
using Ambev.Core.Application.UseCases.Commands.Sale.CreateBranch;
using Ambev.Core.Application.UseCases.Queries.GetBranchsQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.General.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class BranchsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BranchsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateBranchResponse>> Create(CreateBranchRequest request,
                                                             CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBranchRequest request,
                                                CancellationToken cancellationToken)
        {
            request.UpdateBranchRequestIdContext(id);
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id,
                                                CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteBranchRequest(id), cancellationToken);
            return Ok(response);
        }

        [HttpGet("/branchs/{id}")]
        public async Task<ActionResult<List<string>>> GetById(int id,CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetBranchByIdRequest(id), cancellationToken);
            return Ok(response);
        }

        [HttpGet("/branchs")]
        public async Task<ActionResult<List<GetBranchsQueryResponse>>> GetBranchsQuery(CancellationToken cancellationToken, int _page = 1, int _size = 10, [FromQuery] Dictionary<string, string> filters =  null, string _order = "id asc")
        {
            filters = filters ?? new Dictionary<string, string>();
            filters = HttpContext.Request.Query
            .Where(q => q.Key != "_page" && q.Key != "_size" && q.Key != "_order")
            .ToDictionary(q => q.Key, q => q.Value.ToString());

            var response = await _mediator.Send(new GetBranchsQueryRequest( _page, _size, _order, filters), cancellationToken);
            return Ok(response);
        }
    }
}
