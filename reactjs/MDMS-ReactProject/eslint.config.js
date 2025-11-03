module.exports = {
  root: true,
  env: { browser: true, es2020: true },
  extends: [
    'eslint:recommended',
    'plugin:react/recommended', // Add this for React rules
    'plugin:react/jsx-runtime', // Add this for new JSX transform
    'plugin:react-hooks/recommended',
    'plugin:prettier/recommended', // Adds Prettier rules
  ],
  ignorePatterns: ['dist', '.eslintrc.cjs'],
  parserOptions: { ecmaVersion: 'latest', sourceType: 'module' },
  settings: { react: { version: '18.2' } },
  plugins: ['react-refresh', 'prettier'], // Adds Prettier plugin
  rules: {
    'prettier/prettier': 'warn', // Show Prettier errors as warnings
    'react-refresh/only-export-components': [
      'warn',
      { allowConstantExport: true },
    ],
    'react/prop-types': 'off', // We'll disable prop-types for this learning project
  },
};