import React, { useState, useEffect } from 'react';
import toast from 'react-hot-toast';
import apiClient from '../services/apiClient';
import '../styles/Modal.css';
import '../styles/Table.css'; // For .btn styles

export default function EnterpriseInviteUserModal({ onClose, onUserInvited }) {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    role: 'ZoneLevel', // Default to ZoneLevel
    zoneId: '',
  });
  const [zones, setZones] = useState([]);
  const [loading, setLoading] = useState(false);

  // Fetch all available zones to populate the dropdown
  useEffect(() => {
    const fetchZones = async () => {
      try {
        const response = await apiClient.get('/zones');
        setZones(response.data);
        if (response.data.length > 0) {
          // Set a default zone
          setFormData(f => ({ ...f, zoneId: response.data[0].id }));
        }
      } catch (error) {
        console.error("Failed to fetch zones", error);
      }
    };
    fetchZones();
  }, []);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    
    try {
      const newUser = {
        ...formData,
        zoneId: formData.role === 'EndUser' ? formData.zoneId : null, // Only EndUsers get a zoneId
        password: 'password123', // Set a default password
        status: 'Active',
      };
      
      const response = await apiClient.post('/users', newUser);
      
      onUserInvited(response.data);
      toast.success('User invited successfully!');
      onClose();
    } catch (error) {
      toast.error('Failed to invite user.');
      console.error('Invite error:', error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="modal-backdrop" onClick={onClose}>
      <div className="modal-content" onClick={(e) => e.stopPropagation()}>
        <form onSubmit={handleSubmit}>
          <div className="modal-header">
            <h2>Invite user</h2>
            <p style={{color: 'var(--color-text-secondary)', marginTop: '0.5rem'}}>This is a dialogue for inviting user.</p>
            <button type="button" className="modal-close-btn" onClick={onClose}>
              &times;
            </button>
          </div>

          <div className="modal-body">
            <div className="form-field">
              <label htmlFor="email">email</label>
              <input
                id="email"
                name="email"
                type="email"
                value={formData.email}
                onChange={handleChange}
                placeholder="user@gmail.com"
                required
              />
            </div>
            
            <div className="form-field">
              <label htmlFor="name">name</label>
              <input
                id="name"
                name="name"
                type="text"
                value={formData.name}
                onChange={handleChange}
                placeholder="Enter user's name"
                required
              />
            </div>

            <div className="form-field">
              <label htmlFor="role">role</label>
              <select
                id="role"
                name="role"
                value={formData.role}
                onChange={handleChange}
              >
                <option value="ZoneLevel">Zone Admin</option>
                <option value="EndUser">End User</option>
                <option value="EnterpriseLevel">Enterprise Admin</option>
              </select>
            </div>

            {/* Only show Zone selector if role is EndUser */}
            {formData.role === 'EndUser' && (
              <div className="form-field">
                <label htmlFor="zoneId">zone</label>
                <select
                  id="zoneId"
                  name="zoneId"
                  value={formData.zoneId}
                  onChange={handleChange}
                  required
                >
                  <option value="" disabled>Select a zone</option>
                  {zones.map(zone => (
                    <option key={zone.id} value={zone.id}>
                      {zone.zoneName}
                    </option>
                  ))}
                </select>
              </div>
            )}
          </div>

          <div className="modal-footer">
            <button type="submit" className="btn btn-primary" disabled={loading} style={{width: '100%', background: '#222'}}>
              {loading ? 'Inviting...' : 'Invite user'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}