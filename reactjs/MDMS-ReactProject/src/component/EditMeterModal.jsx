import { useState, useEffect } from 'react';
import toast from 'react-hot-toast';
import apiClient from '../services/apiClient';
import '../styles/Modal.css';
import '../styles/Table.css'; // For .btn styles

export default function EditMeterModal({ meter, onClose, onSave }) {
  const [formData, setFormData] = useState({
    status: 'Active',
    userId: '',
  });
  const [loading, setLoading] = useState(false);

  // Pre-fill the form when the 'meter' prop changes
  useEffect(() => {
    if (meter) {
      setFormData({
        status: meter.status,
        userId: meter.userId,
      });
    }
  }, [meter]);

  if (!meter) return null;

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    
    try {
      const response = await apiClient.patch(`/meters/${meter.id}`, {
        status: formData.status,
        userId: parseInt(formData.userId, 10), // Ensure userId is a number
      });
      
      onSave(response.data); // Pass the updated meter back to the parent
      toast.success('Meter updated successfully!');
      onClose();
    } catch (error) {
      toast.error('Failed to update meter.');
      console.error('Save error:', error);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="modal-backdrop" onClick={onClose}>
      <div className="modal-content" onClick={(e) => e.stopPropagation()}>
        <form onSubmit={handleSubmit}>
          <div className="modal-header">
            <h2>Edit Meter {meter.id}</h2>
            <button type="button" className="modal-close-btn" onClick={onClose}>
              &times;
            </button>
          </div>

          <div className="modal-body">
            <div className="form-field">
              <label htmlFor="userId">Owner (User ID)</label>
              <input
                id="userId"
                name="userId"
                type="number"
                value={formData.userId}
                onChange={handleChange}
                placeholder="Enter User ID"
              />
            </div>
            
            <div className="form-field">
              <label htmlFor="status">Status</label>
              <select
                id="status"
                name="status"
                value={formData.status}
                onChange={handleChange}
              >
                <option value="Active">Active</option>
                <option value="De-Activated">De-Activated</option>
              </select>
            </div>
          </div>

          <div className="modal-footer">
            <button type="button" className="btn btn-secondary" onClick={onClose}>
              Cancel
            </button>
            <button type="submit" className="btn btn-primary" disabled={loading}>
              {loading ? 'Saving...' : 'Save Changes'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}