namespace ApiLayer.Models
{
    /// <summary>
    /// Supplier Data Transfer Object for API responses
    /// </summary>
    public class SupplierDto
    {
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierAddress { get; set; }
        public string? SupplierAbn { get; set; }
        public string? SupplierContact { get; set; }
        public bool IsActive { get; set; }
    }

    /// <summary>
    /// Request model for creating a new supplier
    /// </summary>
    public class CreateSupplierRequest
    {
        public string? SupplierName { get; set; }
        public string? SupplierAddress { get; set; }
        public string? SupplierAbn { get; set; }
        public string? SupplierContact { get; set; }
    }

    /// <summary>
    /// Request model for updating a supplier
    /// </summary>
    public class UpdateSupplierRequest
    {
        public string? SupplierName { get; set; }
        public string? SupplierAddress { get; set; }
        public string? SupplierAbn { get; set; }
        public string? SupplierContact { get; set; }
    }

    /// <summary>
    /// Generic API response wrapper
    /// </summary>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
    }
}
