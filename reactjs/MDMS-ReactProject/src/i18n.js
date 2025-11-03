import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import HttpApi from 'i18next-http-backend';

i18n
  .use(HttpApi) // Loads translations from a server (e.g., /public/locales)
  .use(initReactI18next) // Passes i18n down to react-i18next
  .init({
    lng: 'en', // Default language
    fallbackLng: 'en', // Fallback language if one is missing
    debug: true, // Logs info to console
    
    // Namespaces (optional, but good for organizing)
    ns: ['common'],
    defaultNS: 'common',
    
    backend: {
      // Path to translation files
      loadPath: '/locales/{{lng}}/{{ns}}.json',
    },
    
    interpolation: {
      escapeValue: false, // React already safes from XSS
    },
  });

export default i18n;