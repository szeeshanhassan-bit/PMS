/**
 * API Client Helper for communicating with REST API
 * Use this in your ASPX pages to make AJAX calls
 */

class ApiClient {
    constructor(baseUrl = '/api') {
        this.baseUrl = baseUrl;
    }

    /**
     * Make a GET request
     * @param {string} endpoint - The API endpoint (e.g., 'suppliers' or 'suppliers/1')
     * @returns {Promise}
     */
    async get(endpoint) {
        try {
            const response = await fetch(`${this.baseUrl}/${endpoint}`, {
                method: 'GET',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            return await response.json();
        } catch (error) {
            console.error('GET request failed:', error);
            throw error;
        }
    }

    /**
     * Make a POST request
     * @param {string} endpoint - The API endpoint
     * @param {Object} data - Data to send in request body
     * @returns {Promise}
     */
    async post(endpoint, data) {
        try {
            const response = await fetch(`${this.baseUrl}/${endpoint}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            return await response.json();
        } catch (error) {
            console.error('POST request failed:', error);
            throw error;
        }
    }

    /**
     * Make a PUT request
     * @param {string} endpoint - The API endpoint (e.g., 'suppliers/1')
     * @param {Object} data - Data to send in request body
     * @returns {Promise}
     */
    async put(endpoint, data) {
        try {
            const response = await fetch(`${this.baseUrl}/${endpoint}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(data)
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            return await response.json();
        } catch (error) {
            console.error('PUT request failed:', error);
            throw error;
        }
    }

    /**
     * Make a DELETE request
     * @param {string} endpoint - The API endpoint (e.g., 'suppliers/1')
     * @returns {Promise}
     */
    async delete(endpoint) {
        try {
            const response = await fetch(`${this.baseUrl}/${endpoint}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            return await response.json();
        } catch (error) {
            console.error('DELETE request failed:', error);
            throw error;
        }
    }
}

// Initialize global API client
const apiClient = new ApiClient();

/**
 * Suppliers API Helper Functions
 * Use these functions in your ASPX pages
 */

class SuppliersApi {
    /**
     * Get all suppliers
     * @returns {Promise<Array>}
     */
    static async getAllSuppliers() {
        return await apiClient.get('suppliers');
    }

    /**
     * Get a specific supplier by ID
     * @param {number} id - Supplier ID
     * @returns {Promise<Object>}
     */
    static async getSupplier(id) {
        if (!id || id <= 0) {
            throw new Error('Invalid supplier ID');
        }
        return await apiClient.get(`suppliers/${id}`);
    }

    /**
     * Create a new supplier
     * @param {Object} supplier - Supplier object with properties: supplierName, supplierAddress, supplierAbn, supplierContact
     * @returns {Promise<Object>}
     */
    static async createSupplier(supplier) {
        if (!supplier || !supplier.supplierName) {
            throw new Error('Supplier name is required');
        }

        const request = {
            supplierName: supplier.supplierName,
            supplierAddress: supplier.supplierAddress || '',
            supplierAbn: supplier.supplierAbn || '',
            supplierContact: supplier.supplierContact || ''
        };

        return await apiClient.post('suppliers', request);
    }

    /**
     * Update an existing supplier
     * @param {number} id - Supplier ID
     * @param {Object} supplier - Updated supplier data
     * @returns {Promise<Object>}
     */
    static async updateSupplier(id, supplier) {
        if (!id || id <= 0) {
            throw new Error('Invalid supplier ID');
        }

        if (!supplier || !supplier.supplierName) {
            throw new Error('Supplier name is required');
        }

        const request = {
            supplierName: supplier.supplierName,
            supplierAddress: supplier.supplierAddress || '',
            supplierAbn: supplier.supplierAbn || '',
            supplierContact: supplier.supplierContact || ''
        };

        return await apiClient.put(`suppliers/${id}`, request);
    }

    /**
     * Delete a supplier
     * @param {number} id - Supplier ID
     * @returns {Promise<Object>}
     */
    static async deleteSupplier(id) {
        if (!id || id <= 0) {
            throw new Error('Invalid supplier ID');
        }
        return await apiClient.delete(`suppliers/${id}`);
    }
}

/**
 * Example Usage in ASPX pages:
 * 
 * // Get all suppliers
 * document.getElementById('loadSuppliers').addEventListener('click', async function() {
 *     try {
 *         const suppliers = await SuppliersApi.getAllSuppliers();
 *         console.log('Suppliers:', suppliers);
 *         // Populate your table or list here
 *     } catch (error) {
 *         alert('Error loading suppliers: ' + error.message);
 *     }
 * });
 *
 * // Create a new supplier
 * document.getElementById('btnAdd').addEventListener('click', async function() {
 *     try {
 *         const newSupplier = {
 *             supplierName: document.getElementById('txtSupplierName').value,
 *             supplierAddress: document.getElementById('txtAddress').value,
 *             supplierAbn: document.getElementById('txtAbn').value,
 *             supplierContact: document.getElementById('txtContact').value
 *         };
 *         const response = await SuppliersApi.createSupplier(newSupplier);
 *         console.log('Supplier created:', response);
 *         alert('Supplier created successfully!');
 *     } catch (error) {
 *         alert('Error creating supplier: ' + error.message);
 *     }
 * });
 *
 * // Update a supplier
 * document.getElementById('btnUpdate').addEventListener('click', async function() {
 *     try {
 *         const supplierId = document.getElementById('hdnSupplierId').value;
 *         const updatedSupplier = {
 *             supplierName: document.getElementById('txtSupplierName').value,
 *             supplierAddress: document.getElementById('txtAddress').value,
 *             supplierAbn: document.getElementById('txtAbn').value,
 *             supplierContact: document.getElementById('txtContact').value
 *         };
 *         const response = await SuppliersApi.updateSupplier(supplierId, updatedSupplier);
 *         console.log('Supplier updated:', response);
 *         alert('Supplier updated successfully!');
 *     } catch (error) {
 *         alert('Error updating supplier: ' + error.message);
 *     }
 * });
 *
 * // Delete a supplier
 * document.getElementById('btnDelete').addEventListener('click', async function() {
 *     try {
 *         const supplierId = document.getElementById('hdnSupplierId').value;
 *         if (confirm('Are you sure you want to delete this supplier?')) {
 *             const response = await SuppliersApi.deleteSupplier(supplierId);
 *             console.log('Supplier deleted:', response);
 *             alert('Supplier deleted successfully!');
 *         }
 *     } catch (error) {
 *         alert('Error deleting supplier: ' + error.message);
 *     }
 * });
 */
