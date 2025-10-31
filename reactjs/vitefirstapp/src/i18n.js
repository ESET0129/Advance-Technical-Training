import i18next from 'i18next';
import I18nextBrowserLanguageDetector from 'i18next-browser-languagedetector';
i18next.use(I18nextBrowserLanguageDetector);
import { initReactI18next } from 'react-i18next';
import { Translation } from 'react-i18next';


import elang from './locales/el/translation.json';
import enlang from './locales/en/translation.json';
import frlang from './locales/fr/translation.json';

const resource = {
    er : enlang,
    en : enlang,
    fr : frlang,
    
}

I18n.use(I18nextBrowserLanguageDetector)
I18n.use(initReactI18next)
i18n.use(LanguageDetector)
    .use(initReactI18next)
    .init({
        resources,
        fallbackLng: 'en',
        interpolation: {
            escapeValue: false,
        },
    });


export default i18next;