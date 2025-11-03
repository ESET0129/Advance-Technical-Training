import { useEffect } from 'react';
import { useSettingsStore } from '../store/settingsStore';
import { FaSun, FaMoon } from 'react-icons/fa';

export default function ThemeToggle() {
  const { theme, toggleTheme } = useSettingsStore();

  // Apply the theme to the <html> tag
  useEffect(() => {
    document.documentElement.setAttribute('data-theme', theme);
  }, [theme]);

  return (
    <button
      className="theme-toggle-btn"
      onClick={toggleTheme}
      data-theme={theme}
      aria-label={`Switch to ${theme === 'light' ? 'dark' : 'light'} mode`}
    >
      <FaSun className="theme-toggle-icon sun" />
      <FaMoon className="theme-toggle-icon moon" />
      <span className="theme-toggle-ball"></span>
    </button>
  );
}