import { useState } from 'react';
import '../styles/Settings.css'; // Import our new styles
import '../styles/Profile.css'; // For form-field

// Reusable Slider Component
const ThresholdSlider = ({ label, min, max, value, onChange }) => (
  <div className="setting-card">
    <h3>{label}</h3>
    <div className="slider-container">
      <input
        type="range"
        min={min}
        max={max}
        value={value}
        className="slider-range"
        onChange={onChange}
      />
      <div className="slider-labels">
        <span>{min}</span>
        <span>{max}</span>
      </div>
    </div>
  </div>
);

export default function ZoneSettingsTab() {
  // Mock state for the settings
  const [highThreshold, setHighThreshold] = useState(750);
  const [lowThreshold, setLowThreshold] = useState(250);
  const [readFrequency, setReadFrequency] = useState(4);
  const [inactiveDuration, setInactiveDuration] = useState('Sunday');

  return (
    <>
      <div className="settings-grid">
        <ThresholdSlider
          label="High Consumption Threshold (kWh)"
          min={0}
          max={1000}
          value={highThreshold}
          onChange={(e) => setHighThreshold(e.target.value)}
        />
        <ThresholdSlider
          label="Low Consumption Threshold (kWh)"
          min={0}
          max={1000}
          value={lowThreshold}
          onChange={(e) => setLowThreshold(e.target.value)}
        />
        <ThresholdSlider
          label="Abnormal Reading Frequency (hours)"
          min={0}
          max={10}
          value={readFrequency}
          onChange={(e) => setReadFrequency(e.target.value)}
        />
        
        {/* Inactive Meters Duration Card */}
        <div className="setting-card">
          <h3>Inactive Meters Duration (days)</h3>
          <div className="form-field">
            <label htmlFor="inactive-day">day</label>
            <input
              type="text"
              id="inactive-day"
              value={inactiveDuration}
              onChange={(e) => setInactiveDuration(e.target.value)}
            />
          </div>
        </div>
      </div>
      
      <div style={{ maxWidth: '450px', margin: '2rem auto 0' }}>
        <button type="submit" className="form-submit-btn">
          Save and continue
        </button>
      </div>
    </>
  );
}