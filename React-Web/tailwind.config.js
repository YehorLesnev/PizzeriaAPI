import {nextui} from '@nextui-org/theme'

const defaultTheme = require("tailwindcss/defaultTheme");
const colors = require("tailwindcss/colors");
const {
  default: flattenColorPalette,
} = require("tailwindcss/lib/util/flattenColorPalette");

/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './pages/**/*.{js,ts,jsx,tsx,mdx}',
    './components/**/*.{js,ts,jsx,tsx,mdx}',
    './app/**/*.{js,ts,jsx,tsx,mdx}',
    './node_modules/@nextui-org/theme/dist/**/*.{js,ts,jsx,tsx}'
  ],
  theme: {
    extend: {},
  },
  darkMode: "class",
  plugins: [ nextui(
      {
        themes: {
          light: {
            colors: {
              background: "#FFFFFF", // or DEFAULT
              foreground: "#11181C", // or 50 to 900 DEFAULT
              primary: {
                //... 50 to 900
                foreground: "#FFFFFF",
                DEFAULT: "orange",
              },
              secondary: {
                DEFAULT: "green"
              }
              // ... rest of the colors
            }
          }
        }
      },), addVariablesForColors],
}


function addVariablesForColors({ addBase, theme }) {
  let allColors = flattenColorPalette(theme("colors"));
  let newVars = Object.fromEntries(
      Object.entries(allColors).map(([key, val]) => [`--${key}`, val])
  );

  addBase({
    ":root": newVars,
  });
}
