/** @type {import('tailwindcss').Config} */
const defaultTheme = require('tailwindcss/defaultTheme');
const { iconsPlugin, dynamicIconsPlugin } = require("@egoist/tailwindcss-icons")
const plugin = require('tailwindcss/plugin');

const doubleSpacing = {};
Object.entries(defaultTheme.spacing).forEach(([key, value]) => {
    if (typeof value === 'string') {
        const s = value + '';
        if (s.endsWith('rem')) {
            const n = parseFloat(s.substring(0, s.length - 3)) * 2;
            value = `${n}rem`;
        }
    }
    doubleSpacing[key] = value;
});
doubleSpacing['label'] = 'calc(0.4375rem + 1px)';



module.exports = {
    content: ["./**/*.razor"],
    theme: {
        screens: {
            sm: '576px',
            md: '768px',
            lg: '992px',
            xl: '1320px',
            '2xl': '1536px',
        },
        colors: {
            transparent: 'transparent',
            current: 'currentColor',
            g: {
                670: '#002929',
                600: '#003737',
                500: '#004c4c',
                400: '#005e5d',
                300: '#00716b',
                200: '#00857a',
                100: '#37aba1',
                75: '#86d1cd',
                65: '#ade0df',
                55: '#d8f0ef',
                50: '#edf7f7',
            },
            y: {
                500: '#e6b400',
                400: '#f3c000',
                300: '#ffd200',
                200: '#ffe263',
                100: '#ffea8f',
                75: '#fff2ba',
                65: '#fff6cc',
                55: '#fff9de',
                50: '#fffbe6',
            },
            gm: {
                500: '#2d4d4d',
                400: '#3c5e5e',
                300: '#4f706f',
                200: '#668582',
                100: '#91aba9',
                75: '#c0d1d0',
                65: '#d3e0e0',
                55: '#e9f0f0',
                50: '#f2f7f7',
            },
            n: {
                670: '#222',
                500: '#4d4d4d',
                400: '#666',
                300: '#808080',
                200: '#999',
                100: '#b3b3b3',
                75: '#ccc',
                65: '#dedede',
                55: '#f0f0f0',
                50: '#f9f9f9',
            },
            white: '#fff',
            positive: {
                500: '#467a06',
                400: '#528217',
                300: '#76a340',
                200: '#8db55c',
                100: '#bdd99c',
                75: '#d5e8be',
                65: '#e3f0d3',
                55: '#edf7e1',
                50: '#f2fae8',
            },
            info: {
                500: '#005cde',
                400: '#0070e9',
                300: '#2d97ed',
                200: '#5ab8f2',
                100: '#89d3f5',
                75: '#b9e6fa',
                65: '#d1f0fc',
                55: '#e8f8ff',
                50: '#edfaff',
            },
            negative: {
                500: '#c21700',
                400: '#d21b02',
                300: '#db402c',
                200: '#e36959',
                100: '#ed9387',
                75: '#ffc9c2',
                65: '#ffdcd8',
                55: '#fff1f0',
                50: '#fff4f2',
            },
            warning: {
                500: '#e65c00',
                400: '#f60',
                300: '#ff822e',
                200: '#ff9f5e',
                100: '#ffba8c',
                75: '#ffd7bd',
                65: '#ffe4d1',
                55: '#fff1e8',
                50: '#fff3eb',
            },
            privacy: {
                500: '#561391',
                400: '#6927a3',
                300: '#8145b5',
                200: '#9a67c7',
                100: '#ba93db',
                75: '#d8c0ed',
                65: '#e7d7f6',
                55: '#f7f0ff',
                50: '#f9f2ff',
            },
        },
        spacing: doubleSpacing,
        extend: {
            boxShadow: {
                button: '#c0d1d0 0 0 0 1px inset',
                card: 'rgba(34, 34, 34, 0.08) 0px 1px 2px, rgba(0, 0, 0, 0.04) 0px -1px 4px',
                modal: 'rgb(34 34 34 / 40%) 0px 12px 24px, rgb(34 34 34 / 2%) 0px 0px 10px',
                popover: 'rgba(34, 34, 34, 0.02) 0px 0px 6px, rgba(34, 34, 34, 0.15) 0px 4px 8px',
                'toggle-slider':
                    '0px 3px 8px rgba(0, 0, 0, 0.15), 0px 1px 1px rgba(0, 0, 0, 0.16), 0px 3px 1px rgba(0, 0, 0, 0.1)',
            },
            fontFamily: {
                sans: ['Roboto', ...defaultTheme.fontFamily.sans],
                condensed: ['"Roboto Condensed"'],
            },
            fontSize: {
                lg: ['1.125rem', '1.5rem'],
                xl: ['1.25rem', '1.5rem'],
                '2xl': ['1.5rem', '2rem'],
                '3xl': ['1.75rem', '2rem'],
                '4xl': ['2rem', '2.5rem'],
                '5xl': ['2.5rem', '3rem'],
                '6xl': ['3.5rem', '4rem'],
            },
            width: {
                input: '300px',
            },
            maxWidth: {
                input: '300px',
            },
            keyframes: {
                jump: {
                    '0%, 100%': {
                        // fontSize: '100%',
                        transform: 'scale(100%)',
                    },
                    '10%': {
                        // fontSize: '80%',
                        // color: '#89d3f5',
                        transform: 'scale(80%)',
                    },
                    '50%': {
                        // fontSize: '120%',
                        // color: '#2d97ed',
                        transform: 'scale(120%)',
                    },
                },
                flash: {
                    '0%, 50%, 100%': {
                        // fontSize: '100%',
                        transform: 'scale(100%)',
                        opacity: 1,
                    },
                    '25%': {
                        // fontSize: '80%',
                        // color: '#89d3f5',
                        transform: 'scale(80%)',
                        opacity: 0.7,
                    },
                    '75%': {
                        // fontSize: '120%',
                        // color: '#2d97ed',
                        transform: 'scale(120%)',
                        opacity: 0.7,
                    },
                },
            },
            animation: {
                jump: 'jump .3s both 3',
                flash: 'flash .4s both 3',
            },
        },
    },
    plugins: [
        require('@tailwindcss/forms'),
        require('@tailwindcss/typography'),
        require('@tailwindcss/aspect-ratio'),
        plugin(function ({ matchUtilities, theme }) {
            matchUtilities(
                {
                    'bg-gradient': (angle) => ({
                        'background-image': `linear-gradient(${angle}, var(--tw-gradient-stops))`,
                    }),
                },
                {
                    // values from config and defaults you wish to use most
                    values: Object.assign(
                        theme('bgGradientDeg', {}), // name of config key. Must be unique
                        {
                            10: '10deg', // bg-gradient-10
                            15: '15deg',
                            20: '20deg',
                            25: '25deg',
                            30: '30deg',
                            45: '45deg',
                            60: '60deg',
                            90: '90deg',
                            120: '120deg',
                            135: '135deg',
                        }
                    ),
                }
            );
        }),
        function ({ addVariant }) {
            addVariant('initial', 'html :where(&)');
            addVariant('warn', '&.is-warn');
            addVariant('peer-warn', ':merge(.peer).is-warn ~ &');
            addVariant('info', '&.is-info');
            addVariant('peer-info', ':merge(.peer).is-info ~ &');
            addVariant('valid', '&.is-valid');
            addVariant('peer-valid', ':merge(.peer).is-valid ~ &');
            addVariant('invalid', '&.is-invalid');
            addVariant('peer-invalid', ':merge(.peer).is-invalid ~ &');
            addVariant('negative', '&.is-negative');
        },
        iconsPlugin(),
        dynamicIconsPlugin()
    ],
}