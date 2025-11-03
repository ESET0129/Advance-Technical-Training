//import React from 'react';
import React, { Suspense } from 'react';
import ReactDOM from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import { Toaster } from 'react-hot-toast';
import App from './App.jsx';
import './index.css';
import { registerChartComponents } from './services/chartService';

import './i18n.js';
import './styles/Theme.css';

registerChartComponents(); // Run chart registration

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    
    <Suspense fallback="...loading"> 
      <BrowserRouter>
        <App />
        <Toaster position="top-right" />
      </BrowserRouter>
    </Suspense>
  </React.StrictMode>
);