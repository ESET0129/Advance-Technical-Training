import React, { useState, useEffect } from 'react';
import toast from 'react-hot-toast';
import apiClient from '../services/apiClient';
import '../styles/Modal.css';
import '../styles/Table.css'; // For .btn styles

export default function AddZoneModal({ onClose, onZoneAdded }) {
  const [formData, setFormData] = useState({
    zoneName: '',
    adminId: '',
    location: '',
    description: '',
  });
  const [admins, setAdmins] = useState([]); // To populate the dropdown
  const [loading, setLoading] = useState(false);

  // Fetch users who can be admins (e.g., ZoneLevel roles)
  useEffect(() => {
    const fetchAdmins = async () => {
      try {
        // We fetch all users and you can assign anyone, or filter by role
        const response = await apiClient.get('/users', {
          params: { role_ne: 'EndUser' } // Get users who are NOT EndUsers
        });
        setAdmins(response.data);
        if (response.data.length > 0) {
          // Set a default admin
          setFormData(f => ({ ...f, adminId: response.data[0].id }));
        }
      } catch (error) {
        console.error("Failed to fetch admins", error);
      }
    };
    fetchAdmins();
  }, []);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    
    try {
      const newZone = {
        ...formData,
        adminId: parseInt(formData.adminId), // Ensure it's a number
        status: 'Active', // Default to Active
        totalMeters: 0, // Default to 0
      };
      
      const response = await apiClient.post('/zones', newZone);
      
      onZoneAdded(response.data);
      toast.success('Zone added successfully!');
      onClose();
    } catch (error) {
      toast.error('Failed to add zone.');
      console.error('Add zone error:', error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="modal-backdrop" onClick={onClose}>
      <div className="modal-content" onClick={(e) => e.stopPropagation()}>
        <form onSubmit={handleSubmit}>
          <div className="modal-header">
            <h2>Add zone</h2>
            <button type="button" className="modal-close-btn" onClick={onClose}>
              &times;
            </button>
          </div>

          <div className="modal-body">
            <div className="form-field">
              <label htmlFor="zoneName">Zone name</label>
              <input
                id="zoneName"
                name="zoneName"
                type="text"
                value={formData.zoneName}
                onChange={handleChange}
                placeholder="Mangalore"
                required
              />
            </div>

            <div className="form-field">
              <label htmlFor="adminId">Admin</label>
              <select
                id="adminId"
                name="adminId"
                value={formData.adminId}
                onChange={handleChange}
                required
              >
                <option value="" disabled>Select an admin</option>
                {admins.map(admin => (
                  <option key={admin.id} value={admin.id}>
                    {admin.name} ({admin.email})
                  </option>
                ))}
              </select>
            </div>
            
            <div className="form-field">
              <label htmlFor="location">location</label>
              <input
                id="location"
                name="location"
                type="text"
                value={formData.location}
                onChange={handleChange}
                placeholder="Address or pincode"
              />
            </div>
            
            <div className="form-field">
              <label htmlFor="description">description</label>
              <input
                id="description"
                name="description"
                type="text"
                value={formData.description}
                onChange={handleChange}
                placeholder="Description here"
              />
            </div>
          </div>

          <div className="modal-footer">
            <button type="submit" className="btn btn-primary" disabled={loading} style={{width: '100%', background: '#222'}}>
              {loading ? 'Adding...' : 'Add zone'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}