using Microsoft.AspNetCore.Mvc;
using BusinessLayer;
using ApiLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiLayer.Controllers
{
    /// <summary>
    /// Suppliers API Controller
    /// Handles all supplier-related operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SuppliersController : ControllerBase
    {
        private readonly IBusinessManager _businessManager;

        public SuppliersController(IBusinessManager businessManager)
        {
            _businessManager = businessManager;
        }

        /// <summary>
        /// Get all active suppliers
        /// GET: api/suppliers
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<SupplierDto>), 200)]
        [ProducesResponseType(500)]
        public IActionResult GetAllSuppliers()
        {
            try
            {
                var suppliers = _businessManager.GetAllSuppliers();
                return Ok(suppliers);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving suppliers.", details = ex.Message });
            }
        }

        /// <summary>
        /// Get a specific supplier by ID
        /// GET: api/suppliers/{id}
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SupplierDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult GetSupplier(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { error = "Invalid supplier ID" });

                var supplier = _businessManager.GetSupplierById(id);
                if (supplier == null)
                    return NotFound(new { error = $"Supplier with ID {id} not found" });

                return Ok(supplier);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while retrieving the supplier.", details = ex.Message });
            }
        }

        /// <summary>
        /// Create a new supplier
        /// POST: api/suppliers
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<int>), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult CreateSupplier([FromBody] CreateSupplierRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (request == null)
                    return BadRequest(new { error = "Request body is required" });

                if (string.IsNullOrWhiteSpace(request.SupplierName))
                    return BadRequest(new { error = "Supplier name is required" });

                var supplierId = _businessManager.AddSupplier(request);
                return CreatedAtAction(nameof(GetSupplier), new { id = supplierId },
                    new ApiResponse<int> { Success = true, Data = supplierId, Message = "Supplier created successfully" });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while creating the supplier.", details = ex.Message });
            }
        }

        /// <summary>
        /// Update an existing supplier
        /// PUT: api/suppliers/{id}
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult UpdateSupplier(int id, [FromBody] UpdateSupplierRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                if (id <= 0)
                    return BadRequest(new { error = "Invalid supplier ID" });

                if (request == null)
                    return BadRequest(new { error = "Request body is required" });

                var supplier = _businessManager.GetSupplierById(id);
                if (supplier == null)
                    return NotFound(new { error = $"Supplier with ID {id} not found" });

                var success = _businessManager.UpdateSupplier(id, request);
                if (!success)
                    return StatusCode(500, new { error = "Failed to update supplier" });

                return Ok(new ApiResponse<bool> { Success = true, Data = true, Message = "Supplier updated successfully" });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while updating the supplier.", details = ex.Message });
            }
        }

        /// <summary>
        /// Delete (soft delete) a supplier
        /// DELETE: api/suppliers/{id}
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteSupplier(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { error = "Invalid supplier ID" });

                var supplier = _businessManager.GetSupplierById(id);
                if (supplier == null)
                    return NotFound(new { error = $"Supplier with ID {id} not found" });

                var success = _businessManager.DeleteSupplier(id);
                if (!success)
                    return StatusCode(500, new { error = "Failed to delete supplier" });

                return Ok(new ApiResponse<bool> { Success = true, Data = true, Message = "Supplier deleted successfully" });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = "An error occurred while deleting the supplier.", details = ex.Message });
            }
        }
    }
}
