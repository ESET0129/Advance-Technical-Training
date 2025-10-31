import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import I18n from './i18n'
//import { Translation, useTranslation } from 'react-i18next'  




createRoot(document.getElementById('root')).render(
  <StrictMode>
    <App />
  </StrictMode>,
)
