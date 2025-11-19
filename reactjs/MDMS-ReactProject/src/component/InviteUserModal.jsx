import React from 'react';
import { useState } from 'react';
import toast from 'react-hot-toast';
import apiClient from '../services/apiClient';
import { useAuthStore } from '../store/authStore';
import '../styles/Modal.css';
import '../styles/Table.css'; // For .btn styles

export default function InviteUserModal({ onClose, onUserInvited }) {
  const { user } = useAuthStore(); // Get the current zone user
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    role: 'EndUser', // Default new users to EndUser
  });
  const [loading, setLoading] = useState(false);

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    
    // In a real app, the backend would handle the "invitation"
    // Here, we'll just create a new user in our mock API
    try {
      const newUser = {
        ...formData,
        zoneId: user.zoneId, // Assign to the current user's zone
        password: 'password123', // Set a default password
        status: 'Active',
      };
      
      const response = await apiClient.post('/users', newUser);
      
      onUserInvited(response.data); // Pass the new user back to the parent
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
            <button type="button" className="modal-close-btn" onClick={onClose}>
              &times;
            </button>
          </div>

          <div className="modal-body">
            <div className="form-field">
              <label htmlFor="name">Name</label>
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
              <label htmlFor="email">Email</label>
              <input
                id="email"
                name="email"
                type="email"
                value={formData.email}
                onChange={handleChange}
                placeholder="user@example.com"
                required
              />
            </div>
            
            <div className="form-field">
              <label htmlFor="role">Role</label>
              <select
                id="role"
                name="role"
                value={formData.role}
                onChange={handleChange}
              >
                <option value="EndUser">End User</option>
                {/* A zone admin can't create other zone admins */}
              </select>
            </div>

             <div className="form-field">
              <label htmlFor="zone">Zone</label>
              <input
                id="zone"
                name="zone"
                type="text"
                value={user.zoneId} // Display current user's zone
                disabled
              />
            </div>
          </div>

          <div className="modal-footer">
            <button type="submit" className="btn btn-primary" disabled={loading} style={{width: '100%'}}>
              {loading ? 'Inviting...' : 'Invite user'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}