import { useState, useEffect, useRef } from 'react';
import { FaEllipsisV } from 'react-icons/fa';
import '../styles/ActionMenu.css';

export default function ActionMenu({ items }) {
  const [isOpen, setIsOpen] = useState(false);
  const menuRef = useRef(null);

  // Close menu if clicking outside
  useEffect(() => {
    const handleClickOutside = (event) => {
      if (menuRef.current && !menuRef.current.contains(event.target)) {
        setIsOpen(false);
      }
    };
    document.addEventListener('mousedown', handleClickOutside);
    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, []);

  const handleToggle = () => {
    setIsOpen(!isOpen);
  };

  const handleItemClick = (onClick) => {
    setIsOpen(false);
    onClick();
  };

  return (
    <div className="action-menu-container" ref={menuRef}>
      <button className="action-menu-toggle" onClick={handleToggle}>
        <FaEllipsisV />
      </button>
      
      {isOpen && (
        <div className="action-menu-dropdown">
          {items.map((item) => (
            <button
              key={item.label}
              className="action-menu-item"
              onClick={() => handleItemClick(item.onClick)}
            >
              {item.icon}
              {item.label}
            </button>
          ))}
        </div>
      )}
    </div>
  );
}