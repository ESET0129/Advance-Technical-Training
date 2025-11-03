import { useTranslation } from 'react-i18next';

export default function LanguageSelector() {
  const { i18n } = useTranslation();

  const handleChange = (e) => {
    i18n.changeLanguage(e.target.value);
  };

  return (
    <select 
      className="language-selector" 
      value={i18n.language} 
      onChange={handleChange}
    >
      <option value="en">English</option>
      <option value="hi">हिन्दी</option>
    </select>
  );
}