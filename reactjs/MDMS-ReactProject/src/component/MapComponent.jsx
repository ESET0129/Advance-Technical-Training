import { MapContainer, TileLayer, Marker, Popup } from 'react-leaflet';
import 'leaflet/dist/leaflet.css';
import L from 'leaflet'; // Import L to fix a known icon issue

// --- FIX for default marker icon ---
delete L.Icon.Default.prototype._getIconUrl;
L.Icon.Default.mergeOptions({
  iconRetinaUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-icon-2x.png',
  iconUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-icon.png',
  shadowUrl: 'https://unpkg.com/leaflet@1.9.4/dist/images/marker-shadow.png',
});
// --- End of FIX ---

export default function MapComponent({ zones }) {
  const defaultPosition = [12.9141, 74.8560]; // Mangalore

  return (
    <div className="map-container-wrapper">
      <MapContainer center={defaultPosition} zoom={12} className="leaflet-container">
        <TileLayer
          url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
          attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
        />
        {zones.map(zone => (
          <Marker key={zone.id} position={zone.position}>
            <Popup>
              <h4>{zone.name}</h4>
              <p><strong>Active Meters:</strong> {zone.active}</p>
              <p><strong>Inactive Meters:</strong> {zone.inactive}</p>
            </Popup>
          </Marker>
        ))}
      </MapContainer>
    </div>
  );
}